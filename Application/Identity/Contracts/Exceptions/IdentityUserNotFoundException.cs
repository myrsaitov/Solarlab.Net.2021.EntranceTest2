using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Application.Identity.Contracts.Exceptions
{
    public class IdentityUserNotFoundException : NotFoundException
    {
        public IdentityUserNotFoundException(string message) : base(message)
        {
        }
    }
}
