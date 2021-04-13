using WidePictBoard.Application.Identity.Interfaces;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Application.Services.Category.Implementations;
using Moq;
using MapsterMapper;
using WidePictBoard.Application.MapProfiles;
using WidePictBoard.Application.Services.PagedBase.Implementations;
using WidePictBoard.Application.Services.Category.Contracts;

namespace WidePictBoard.Tests.Category
{
    public partial class CategoryServiceV1Test
    {
        private Mock<ICategoryRepository> _categoryRepositoryMock;
        private Mock<ITagRepository> _tagRepositoryMock;
        private Mock<IIdentityService> _identityServiceMock;
        private IMapper _mapper;
        
        private CategoryServiceV1 _categoryServiceV1;
        public CategoryServiceV1Test()
        {
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _tagRepositoryMock = new Mock<ITagRepository>();
            _identityServiceMock = new Mock<IIdentityService>();

            _mapper = new Mapper();
            CategoryMapProfile.GetConfiguredMappingConfig().Compile();
            TagMapProfile.GetConfiguredMappingConfig().Compile();
            UserMapProfile.GetConfiguredMappingConfig().Compile();

            _categoryServiceV1 = new CategoryServiceV1(
                _categoryRepositoryMock.Object,
                _identityServiceMock.Object,
                _mapper);
        }
    }
}
