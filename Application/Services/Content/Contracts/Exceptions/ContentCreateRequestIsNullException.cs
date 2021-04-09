using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Application.Services.Content.Contracts.Exceptions
{
    public sealed class ContentCreateRequestIsNullException : NotFoundException
    {
        private const string MessageTemplate = "CreateRequest is Null!";
        public ContentCreateRequestIsNullException() : base(string.Format(MessageTemplate))
        {
        }
    }
}