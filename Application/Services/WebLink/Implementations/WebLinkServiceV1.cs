using MapsterMapper;
using SL2021.Application.Identity.Interfaces;
using SL2021.Application.Repositories;
using SL2021.Application.Services.WebLink.Interfaces;

namespace SL2021.Application.Services.WebLink.Implementations
{
    public sealed partial class WebLinkServiceV1 : IWebLinkService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWebLinkRepository _webLinkRepository;
        private readonly IContentRepository _contentRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public WebLinkServiceV1(
            IWebLinkRepository webLinkRepository, 
            ICategoryRepository categoryRepository, 
            ITagRepository tagRepository, 
            IIdentityService identityService, 
            IMapper mapper)
        {
            _webLinkRepository = webLinkRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            _identityService = identityService;
            _mapper = mapper;
        }
    }
}