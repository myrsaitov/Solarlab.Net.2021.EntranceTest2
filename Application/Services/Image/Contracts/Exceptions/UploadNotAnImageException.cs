using SL2021.Domain.General.Exceptions;

namespace SL2021.Application.Services.Images.Contracts.Exceptions
{
    public sealed class UploadNotAnImageException : NotFoundException
    {
        private const string MessageTemplate = "Не является изображением";
        public UploadNotAnImageException() : base(string.Format(MessageTemplate))
        {
        }
    }
}