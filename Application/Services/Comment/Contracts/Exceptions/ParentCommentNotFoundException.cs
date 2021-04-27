using SL2021.Domain.General.Exceptions;

namespace SL2021.Application.Services.Comment.Contracts.Exceptions
{
    public sealed class ParentCommentNotFoundException : NotFoundException
    {
        private const string MessageTemplate = "Родительский комментарий с таким ID[{0}] не был найден.";
        public ParentCommentNotFoundException(int? adId) : base(string.Format(MessageTemplate, adId))
        {
        }
    }
}