using SL2021.Domain.General.Exceptions;

namespace SL2021.Application.Services.User.Contracts.Exceptions
{
    public class UserRegisteredException : DomainException
    {
        public UserRegisteredException(string message) : base(message)
        {
        }
    }
}
