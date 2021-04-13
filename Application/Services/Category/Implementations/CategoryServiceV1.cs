using MapsterMapper;
using WidePictBoard.Application.Identity.Interfaces;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Application.Services.Category.Interfaces;

namespace WidePictBoard.Application.Services.Category.Implementations
{
    public sealed partial class CategoryServiceV1 : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public CategoryServiceV1(
            ICategoryRepository categoryRepository, 
            IIdentityService identityService, 
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _identityService = identityService;
            _mapper = mapper;
        }
    }
}