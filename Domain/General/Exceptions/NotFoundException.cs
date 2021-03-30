namespace WidePictBoard.Domain.General.Exceptions
{
    public abstract class NotFoundException : DomainException
    {
        protected NotFoundException(string message) : base(message)
        {
        }
    }
}