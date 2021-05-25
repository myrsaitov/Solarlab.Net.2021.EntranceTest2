using SL2021.Application.Identity.Interfaces;
using SL2021.Application.Repositories;
using SL2021.Application.Services.Image.Interfaces;
using MapsterMapper;

namespace SL2021.Application.Services.Image.Implementations
{
    public sealed partial class ImageServiceV1 : IImageService
    {
        private readonly IIdentityService _identityService;
        private readonly IContentRepository _contentRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;
        public ImageServiceV1(
            IIdentityService identityService,
            IContentRepository contentRepository,
            IImageRepository imageRepository,
            IMapper mapper) 
        {
            _identityService = identityService;
            _contentRepository = contentRepository;
            _imageRepository = imageRepository;
            _mapper = mapper;
        }
    }
}