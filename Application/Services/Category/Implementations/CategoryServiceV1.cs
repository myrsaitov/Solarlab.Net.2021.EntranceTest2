using MapsterMapper;
using SL2021.Application.Identity.Interfaces;
using SL2021.Application.Repositories;
using SL2021.Application.Services.Category.Interfaces;

namespace SL2021.Application.Services.Category.Implementations
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