using SL2021.Application.Identity.Interfaces;
using SL2021.Application.Repositories;
using SL2021.Application.Services.Category.Implementations;
using Moq;
using MapsterMapper;
using SL2021.Application.MapProfiles;
using Mapster;
using System.Linq.Expressions;

namespace SL2021.Tests.Category
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

            TypeAdapterConfig.GlobalSettings.Compiler = exp => exp.CompileWithDebugInfo();
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
