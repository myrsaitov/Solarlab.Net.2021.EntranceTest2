using SL2021.Domain.General.Exceptions;

namespace SL2021.Application.Services.User.Contracts.Exceptions
{
    public sealed class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(string message) : base(message)
        {

        }
    }
}