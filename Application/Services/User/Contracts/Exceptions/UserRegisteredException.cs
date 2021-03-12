using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Application.Services.User.Contracts.Exceptions
{
    public class UserRegisteredException : DomainException
    {
        public UserRegisteredException(string message) : base(message)
        {
        }
    }
}
