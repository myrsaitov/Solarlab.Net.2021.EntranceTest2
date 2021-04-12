using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Application.Services.Category.Contracts.Exceptions
{
    public sealed class CategoryGetByIdRequestIsNullException : NotFoundException
    {
        private const string MessageTemplate = "Category GetByIdRequest is Null!";
        public CategoryGetByIdRequestIsNullException() : base(string.Format(MessageTemplate))
        {
        }
    }
}