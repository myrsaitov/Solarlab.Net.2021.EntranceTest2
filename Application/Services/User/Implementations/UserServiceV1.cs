using System;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Common;
using WidePictBoard.Application.Identity.Contracts;
using WidePictBoard.Application.Identity.Interfaces;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Application.Services.User.Contracts;
using WidePictBoard.Application.Services.User.Contracts.Exceptions;
using WidePictBoard.Application.Services.User.Interfaces;
using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Application.Services.User.Implementations
{
    public sealed class UserServiceV1 : IUserService
    {
        private readonly IRepository<Domain.User, string> _repository;
        private readonly IIdentityService _identityService;

        public UserServiceV1(IRepository<Domain.User, string> repository, IIdentityService identityService)
        {
            _repository = repository;
            _identityService = identityService;
        }

        public async Task<Register.Response> Register(Register.Request registerRequest, CancellationToken cancellationToken)
        {
            var response = await _identityService.CreateUser(
                new CreateUser.Request
                {
                    Username = registerRequest.Username,
                    Password = registerRequest.Password,
                    Role = RoleConstants.UserRole
                }, cancellationToken);

            if (response.IsSuccess)
            {
                var domainUser = new Domain.User
                {
                    Id = response.UserId,
                    FirstName = registerRequest.FirstName,
                    LastName = registerRequest.LastName,
                    MiddleName = registerRequest.MiddleName,
                    CreatedAt = DateTime.UtcNow
                };

                await _repository.Save(domainUser, cancellationToken);

                return new Register.Response
                {
                    UserId = response.UserId
                };
            }

            throw new UserRegisteredException(string.Join(';', response.Errors));
        }

        public async Task Update(Update.Request request, CancellationToken cancellationToken)
        {
            var domainUser = await _repository.FindById(request.Id, cancellationToken);
            if (domainUser == null)
            {
                throw new UserNotFoundException($"Пользователь с идентификатором {request.Id} не найден");
            }

            var currentUserId = await _identityService.GetCurrentUserId(cancellationToken);
            if (domainUser.Id != currentUserId)
            {
                throw new NoRightsException("Нет прав");
            }

            domainUser.FirstName = request.FirstName;
            domainUser.LastName = request.LastName;
            domainUser.MiddleName = request.MiddleName;
            domainUser.UpdatedAt = DateTime.UtcNow;

            await _repository.Save(domainUser, cancellationToken);
        }


    }
}