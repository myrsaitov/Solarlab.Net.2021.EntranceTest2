using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Domain.General.Exceptions
{
    public class NoRightsException : DomainException
    {
        public NoRightsException(string message) : base(message)
        {
        }
    }
}