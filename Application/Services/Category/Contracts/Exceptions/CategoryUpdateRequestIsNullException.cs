using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Application.Services.Category.Contracts.Exceptions
{
    public sealed class CategoryUpdateRequestIsNullException : NotFoundException
    {
        private const string MessageTemplate = "Category UpdateRequest is Null!";
        public CategoryUpdateRequestIsNullException() : base(string.Format(MessageTemplate))
        {
        }
    }
}