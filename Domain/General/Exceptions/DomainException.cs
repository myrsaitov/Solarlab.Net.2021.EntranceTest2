using System;

namespace WidePictBoard.Domain.General.Exceptions
{
    public class DomainException : ApplicationException
    {
        public DomainException(string message) : base(message)
        {
            
        }
    }
}