using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Application.Services.Comment.Contracts.Exceptions
{
    public sealed class CommentCreateRequestIsNullException : NotFoundException
    {
        private const string MessageTemplate = "Comment CreateRequest is Null!";
        public CommentCreateRequestIsNullException() : base(string.Format(MessageTemplate))
        {
        }
    }
}