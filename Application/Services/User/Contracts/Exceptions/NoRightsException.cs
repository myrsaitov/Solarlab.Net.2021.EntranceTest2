using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Application.Services.User.Contracts.Exceptions
{
    public sealed class NoRightsException : Domain.General.Exceptions.NoRightsException
    {
        public NoRightsException(string message) : base(message)
        {
        }
    }
}