namespace WidePictBoard.Domain.General.Exceptions
{
    public abstract class NoRightsException : DomainException
    {
        protected NoRightsException(string message) : base(message)
        {
        }
    }
}