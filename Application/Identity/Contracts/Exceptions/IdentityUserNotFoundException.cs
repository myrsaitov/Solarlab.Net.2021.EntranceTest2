using SL2021.Domain.General.Exceptions;

namespace SL2021.Application.Identity.Contracts.Exceptions
{
    public class IdentityUserNotFoundException : NotFoundException
    {
        public IdentityUserNotFoundException(string message) : base(message)
        {
        }
    }
}
