using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Application.Services.Category.Contracts.Exceptions
{
    public sealed class CategoryDeleteRequestIsNullException : NotFoundException
    {
        private const string MessageTemplate = "Category DeleteRequest is Null!";
        public CategoryDeleteRequestIsNullException() : base(string.Format(MessageTemplate))
        {
        }
    }
}