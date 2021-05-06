using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Services.User.Contracts;
using SL2021.Application.Services.User.Contracts.Exceptions;
using SL2021.Application.Services.User.Interfaces;
using SL2021.Application.Services.User.Validators;
using SL2021.Domain.General.Exceptions;

namespace SL2021.Application.Services.User.Implementations
{
    public sealed partial class UserServiceV1 : IUserService
    {
        public async Task Update(
            Update.Request request, 
            CancellationToken cancellationToken)
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