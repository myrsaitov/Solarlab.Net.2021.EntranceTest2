namespace SL2021.Domain.General.Exceptions
{
    public class ConflictException : DomainException
    {
        public ConflictException(string message) : base(message)
        {
        }
    }
}