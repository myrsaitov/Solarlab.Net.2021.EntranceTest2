using SL2021.Domain.General.Exceptions;

namespace SL2021.Application.Services.Images.Contracts.Exceptions
{
    public sealed class NoImagesException : NotFoundException
    {
        private const string MessageTemplate = "Не загружены файлы";
        public NoImagesException() : base(string.Format(MessageTemplate))
        {
        }
    }
}