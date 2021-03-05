namespace WidePictBoard.Domain.General.Exceptions
{
    public class ConflictException : DomainException
    {
        public ConflictException(string message) : base(message)
        {
        }
    }
}