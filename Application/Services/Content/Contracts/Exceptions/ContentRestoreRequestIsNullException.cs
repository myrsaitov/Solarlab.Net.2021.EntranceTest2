using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Application.Services.Content.Contracts.Exceptions
{
    public sealed class ContentRestoreRequestIsNullException : NotFoundException
    {
        private const string MessageTemplate = "Content RestoreRequest is Null!";
        public ContentRestoreRequestIsNullException() : base(string.Format(MessageTemplate))
        {
        }
    }
}