using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Application.Services.Tag.Contracts.Exceptions
{
    public sealed class TagGetPagedRequestIsNullException : NotFoundException
    {
        private const string MessageTemplate = "Tag GetPagedRequest is Null!";
        public TagGetPagedRequestIsNullException() : base(string.Format(MessageTemplate))
        {
        }
    }
}