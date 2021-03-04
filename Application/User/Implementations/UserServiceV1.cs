using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.User.Contracts;
using WidePictBoard.Application.User.Contracts.Exceptions;
using WidePictBoard.Application.User.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace WidePictBoard.Application.User.Implementations
{
    public sealed class UserServiceV1 : IUserService
    {
        private readonly IConfiguration _configuration;

        private readonly IClaimsAccessor _claimsAccessor;
        private readonly IRepository<Domain.User, int> _repository;

        public UserServiceV1(IRepository<Domain.User, int> repository, IConfiguration configuration, IClaimsAccessor claimsAccessor)
        {
            _repository = repository;
            _configuration = configuration;
            _claimsAccessor = claimsAccessor;
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
            var user = await _repository.FindWhere(u => u.Name == request.Name, cancellationToken);
            if (user == null || !user.Password.Equals(request.Password))
            {
                throw new NoRightsException("Нет прав");
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, request.Name),
                new(ClaimTypes.NameIdentifier, user.Id.ToString())
            };


            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(60),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"])),
                    SecurityAlgorithms.HmacSha256)
            );

            return new Login.Response
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
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