using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Services.User.Contracts;
using WidePictBoard.Application.Services.User.Contracts.Exceptions;
using WidePictBoard.Application.Services.User.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Domain.Specifications;

namespace WidePictBoard.Application.Services.User.Implementations
{
    public sealed class UserServiceV1 : IUserService
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IClaimsAccessor _claimsAccessor;
        private readonly IRepository<Domain.User, int> _repository;

        public UserServiceV1(IRepository<Domain.User, int> repository, IClaimsAccessor claimsAccessor, ITokenGenerator tokenGenerator)
        {
            _repository = repository;
            _claimsAccessor = claimsAccessor;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<Domain.User> GetCurrent(CancellationToken cancellationToken)
        {
            var claim = (await _claimsAccessor.GetCurrentClaims(cancellationToken))
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                ?.Value;

            if (string.IsNullOrWhiteSpace(claim))
            {
                return null;
            }

            var intId = int.Parse(claim);

            var user = await _repository.FindById(intId, cancellationToken);
            if (user == null)
            {
                throw new NoRightsException("Нет прав");
            }

            return user;
        }

        public async Task<Login.Response> Login(Login.Request request, CancellationToken cancellationToken)
        {
            var body = ByUserName.With(request.Name).Body;

            var user = await _repository.FindWhere(ByUserName.With(request.Name), cancellationToken);
            if (user == null || !user.Password.Equals(request.Password))
            {
                throw new NoRightsException("Нет прав");
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, request.Name),
                new(ClaimTypes.NameIdentifier, user.Id.ToString())
            };


            return new Login.Response
            {
                Token = await _tokenGenerator.ObtainTokenFromClaims(claims, cancellationToken)
            };
        }

        public async Task<Register.Response> Register(Register.Request request, CancellationToken cancellationToken)
        {
            var user = new Domain.User
            {
                Name = request.Name,
                Password = request.Password,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.Save(user, cancellationToken);

            return new Register.Response
            {
                UserId = user.Id
            };
        }
    }
}