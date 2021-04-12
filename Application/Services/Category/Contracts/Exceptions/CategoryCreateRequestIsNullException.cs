using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Application.Services.Category.Contracts.Exceptions
{
    public sealed class CategoryCreateRequestIsNullException : NotFoundException
    {
        private const string MessageTemplate = "Category CreateRequest is Null!";
        public CategoryCreateRequestIsNullException() : base(string.Format(MessageTemplate))
        {
        }
    }
}