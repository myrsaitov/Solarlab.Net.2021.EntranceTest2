using SL2021.Application.Identity.Interfaces;
using SL2021.Application.Repositories;
using SL2021.Application.Services.Content.Implementations;
using Moq;
using MapsterMapper;
using SL2021.Application.MapProfiles;
using Mapster;
using System.Linq.Expressions;
using System.Linq;

namespace SL2021.Tests.Content
{
    public partial class ContentServiceV1Test
    {
        private Mock<IContentRepository> _contentRepositoryMock;
        private Mock<ICategoryRepository> _categoryRepositoryMock;
        private Mock<ITagRepository> _tagRepositoryMock;
        private Mock<IIdentityService> _identityServiceMock;
        private IMapper _mapper;
        
        private ContentServiceV1 _contentServiceV1;
        public ContentServiceV1Test()
        {
            _contentRepositoryMock = new Mock<IContentRepository>();
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _tagRepositoryMock = new Mock<ITagRepository>();
            _identityServiceMock = new Mock<IIdentityService>();


            TypeAdapterConfig.GlobalSettings.Compiler = exp => exp.CompileWithDebugInfo();
            _mapper = new Mapper();
            ContentMapProfile.GetConfiguredMappingConfig().Compile();
            CategoryMapProfile.GetConfiguredMappingConfig().Compile();
            CommentMapProfile.GetConfiguredMappingConfig().Compile();
            TagMapProfile.GetConfiguredMappingConfig().Compile();
            UserMapProfile.GetConfiguredMappingConfig().Compile();

            _contentServiceV1 = new ContentServiceV1(
                _contentRepositoryMock.Object,
                _categoryRepositoryMock.Object, 
                _tagRepositoryMock.Object, 
                _identityServiceMock.Object,
                _mapper);
        }
    }
}
