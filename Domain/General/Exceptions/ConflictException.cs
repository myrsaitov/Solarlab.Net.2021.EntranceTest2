namespace WidePictBoard.Domain.General.Exceptions
{
    public abstract class ConflictException : DomainException
    {
        protected ConflictException(string message) : base(message)
        {
        }
    }
}