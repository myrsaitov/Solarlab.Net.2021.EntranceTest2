using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Application.Services.Content.Contracts.Exceptions
{
    public sealed class ContentUpdateRequestIsNullException : NotFoundException
    {
        private const string MessageTemplate = "CreateRequest is Null!";
        public ContentUpdateRequestIsNullException() : base(string.Format(MessageTemplate))
        {
        }
    }
}