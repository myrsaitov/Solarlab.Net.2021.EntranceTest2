using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Application.Services.Content.Contracts.Exceptions
{
    public sealed class ContentDeleteRequestIsNullException : NotFoundException
    {
        private const string MessageTemplate = "Content DeleteRequest is Null!";
        public ContentDeleteRequestIsNullException() : base(string.Format(MessageTemplate))
        {
        }
    }
}