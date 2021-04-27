using SL2021.Domain.General.Exceptions;

namespace SL2021.Application.Services.Category.Contracts.Exceptions
{
    public sealed class CategoryNotFoundException : NotFoundException
    {
        private const string MessageTemplate = "Категория с таким ID[{0}] не была найдена.";
        public CategoryNotFoundException(int adId) : base(string.Format(MessageTemplate, adId))
        {
        }
    }
}