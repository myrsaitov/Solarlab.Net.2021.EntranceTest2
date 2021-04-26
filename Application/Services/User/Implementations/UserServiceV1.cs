using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Identity.Contracts;
using WidePictBoard.Application.Identity.Interfaces;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Application.Services.User.Contracts;
using WidePictBoard.Application.Services.User.Contracts.Exceptions;
using WidePictBoard.Application.Services.User.Interfaces;
using WidePictBoard.Application.Services.User.Validators;
using WidePictBoard.Domain.General.Exceptions;
using MapsterMapper;

namespace WidePictBoard.Application.Services.User.Implementations
{
    public sealed class UserServiceV1 : IUserService
    {
        private readonly IRepository<Domain.User, string> _repository;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public UserServiceV1(
            IRepository<Domain.User, string> repository
            , IIdentityService identityService
            , IMapper mapper)
        {
            _repository = repository;
            _identityService = identityService;
            _mapper = mapper;
        }

        public async Task<Register.Response> Register(Register.Request registerRequest, CancellationToken cancellationToken)
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
                    FirstName = registerRequest.FirstName,
                    LastName = registerRequest.LastName,
                    MiddleName = registerRequest.MiddleName,
                    UserName = registerRequest.Username,
                    CreatedAt = DateTime.UtcNow
                };

                await _repository.Save(domainUser, cancellationToken);

                return _mapper.Map<Register.Response>(response);
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
            domainUser.UserName = request.UserName;
            domainUser.UpdatedAt = DateTime.UtcNow;

            await _repository.Save(domainUser, cancellationToken);
        }
    }
}