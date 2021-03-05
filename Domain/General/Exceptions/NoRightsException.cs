using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Domain.Shared.Exceptions
{
    public class NoRightsException : DomainException
    {
        public NoRightsException(string message) : base(message)
        {
        }
    }
}