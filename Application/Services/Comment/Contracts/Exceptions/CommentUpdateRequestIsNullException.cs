using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Application.Services.Comment.Contracts.Exceptions
{
    public sealed class CommentUpdateRequestIsNullException : NotFoundException
    {
        private const string MessageTemplate = "Comment UpdateRequest is Null!";
        public CommentUpdateRequestIsNullException() : base(string.Format(MessageTemplate))
        {
        }
    }
}