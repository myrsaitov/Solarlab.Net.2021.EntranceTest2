using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Identity.Contracts;
using SL2021.Application.Services.User.Contracts;
using SL2021.Application.Services.User.Contracts.Exceptions;
using SL2021.Application.Services.User.Interfaces;
using SL2021.Application.Services.User.Validators;

namespace SL2021.Application.Services.User.Implementations
{
    public sealed partial class UserServiceV1 : IUserService
    {
        public async Task<Register.Response> Register(
            Register.Request registerRequest, 
            CancellationToken cancellationToken)
        {
            RegisterRequestValidator validator = new();
            var result = await validator.ValidateAsync(registerRequest);

            if (!result.IsValid)
            {
                throw new UserRegisteredException(string.Join(';', result.Errors.Select(x => x.ErrorMessage)));
            }

            CreateUser.Response response = await _identityService.CreateUser(
                _mapper.Map<CreateUser.Request>(registerRequest),
                cancellationToken);

            if (response.IsSuccess)
            {
                var domainUser = new Domain.User
                {
                    Id = response.UserId,
                    UserName = registerRequest.UserName,
                    FirstName = registerRequest.FirstName,
                    LastName = registerRequest.LastName,
                    MiddleName = registerRequest.MiddleName,
                    CreatedAt = DateTime.UtcNow
                };

                await _userRepository.Save(domainUser, cancellationToken);

                return _mapper.Map<Register.Response>(response);
            }

            throw new UserRegisteredException(string.Join(';', response.Errors));
        }
    }
}