using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Application.Services.Category.Contracts.Exceptions
{
    public sealed class CategoryGetPagedRequestIsNullException : NotFoundException
    {
        private const string MessageTemplate = "Category GetPagedRequest is Null!";
        public CategoryGetPagedRequestIsNullException() : base(string.Format(MessageTemplate))
        {
        }
    }
}