using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Application.Services.Comment.Contracts.Exceptions
{
    public sealed class CommentRestoreRequestIsNullException : NotFoundException
    {
        private const string MessageTemplate = "Comment RestoreRequest is Null!";
        public CommentRestoreRequestIsNullException() : base(string.Format(MessageTemplate))
        {
        }
    }
}