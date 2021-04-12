using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Application.Services.Content.Contracts.Exceptions
{
    public sealed class ContentGetByIdRequestIsNullException : NotFoundException
    {
        private const string MessageTemplate = "Content GetByIdRequest is Null!";
        public ContentGetByIdRequestIsNullException() : base(string.Format(MessageTemplate))
        {
        }
    }
}