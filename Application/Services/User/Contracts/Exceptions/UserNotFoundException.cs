using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Application.Services.User.Contracts.Exceptions
{
    public sealed class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(string message) : base(message)
        {

        }
    }
}