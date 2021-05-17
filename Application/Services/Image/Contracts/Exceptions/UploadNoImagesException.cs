using SL2021.Domain.General.Exceptions;

namespace SL2021.Application.Services.Images.Contracts.Exceptions
{
    public sealed class UploadNoImagesException : NotFoundException
    {
        private const string MessageTemplate = "Не загружены файлы";
        public UploadNoImagesException() : base(string.Format(MessageTemplate))
        {
        }
    }
}