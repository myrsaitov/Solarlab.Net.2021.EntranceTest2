using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Application.Services.Category.Contracts.Exceptions
{
    public sealed class CategoryRestoreRequestIsNullException : NotFoundException
    {
        private const string MessageTemplate = "Category RestoreRequest is Null!";
        public CategoryRestoreRequestIsNullException() : base(string.Format(MessageTemplate))
        {
        }
    }
}