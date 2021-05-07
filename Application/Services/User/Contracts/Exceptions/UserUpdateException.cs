using SL2021.Domain.General.Exceptions;

namespace SL2021.Application.Services.User.Contracts.Exceptions
{
    public class UserUpdateException : DomainException
    {
        public UserUpdateException(string message) : base(message)
        {
        }
    }
}
