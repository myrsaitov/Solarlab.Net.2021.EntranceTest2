using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Application.Services.Category.Contracts.Exceptions
{
    public sealed class CategoryNotFoundException : NotFoundException
    {
        private const string MessageTemplate = "Комментарий с таким ID[{0}] не был найден.";
        
        public CategoryNotFoundException(int adId) : base(string.Format(MessageTemplate, adId))
        {
        }
    }
}