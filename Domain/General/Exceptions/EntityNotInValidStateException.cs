namespace WidePictBoard.Domain.General.Exceptions
{
    public abstract class EntityNotInValidStateException : DomainException
    {
        protected EntityNotInValidStateException(string message) : base(message)
        {
        }
    }
}