using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Application.Services.Content.Contracts.Exceptions
{
    public sealed class NoUserForContentCreationException : NoRightsException
    {
        public NoUserForContentCreationException(string message) : base(message)
        {
        }
    }
}