using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Application.Services.Comment.Contracts.Exceptions
{
    public sealed class CommentGetByIdRequestIsNullException : NotFoundException
    {
        private const string MessageTemplate = "Comment GetByIdRequest is Null!";
        public CommentGetByIdRequestIsNullException() : base(string.Format(MessageTemplate))
        {
        }
    }
}