using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Application.Services.Comment.Contracts.Exceptions
{
    public sealed class CommentNotFoundException : NotFoundException
    {
        private const string MessageTemplate = "Комментарий с таким ID[{0}] не был найден.";
        
        public CommentNotFoundException(int adId) : base(string.Format(MessageTemplate, adId))
        {
        }
    }
}