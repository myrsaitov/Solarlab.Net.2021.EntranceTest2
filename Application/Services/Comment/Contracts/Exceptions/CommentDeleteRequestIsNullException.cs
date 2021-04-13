using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Application.Services.Comment.Contracts.Exceptions
{
    public sealed class CommentDeleteRequestIsNullException : NotFoundException
    {
        private const string MessageTemplate = "Comment DeleteRequest is Null!";
        public CommentDeleteRequestIsNullException() : base(string.Format(MessageTemplate))
        {
        }
    }
}