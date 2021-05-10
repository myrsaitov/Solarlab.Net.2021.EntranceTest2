using SL2021.Application.Services.Image.Interfaces;

namespace SL2021.Application.Services.Image.Implementations
{
    public sealed partial class ImageServiceV1 : IImageService
    {
        private readonly IImageRepository _imageRepository;

        public ImageServiceV1(
            IImageRepository imageRepository) 
        {
            _imageRepository = imageRepository;
        }
    }
}