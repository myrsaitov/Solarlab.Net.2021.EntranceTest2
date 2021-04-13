using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Application.Services.Comment.Contracts.Exceptions
{
    public sealed class CommentGetPagedRequestIsNullException : NotFoundException
    {
        private const string MessageTemplate = "Comment GetPagedRequest is Null!";
        public CommentGetPagedRequestIsNullException() : base(string.Format(MessageTemplate))
        {
        }
    }
}