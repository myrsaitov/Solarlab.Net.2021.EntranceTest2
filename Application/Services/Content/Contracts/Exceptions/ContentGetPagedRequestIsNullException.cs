using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Application.Services.Content.Contracts.Exceptions
{
    public sealed class ContentGetPagedRequestIsNullException : NotFoundException
    {
        private const string MessageTemplate = "Content GetPagedRequest is Null!";
        public ContentGetPagedRequestIsNullException() : base(string.Format(MessageTemplate))
        {
        }
    }
}