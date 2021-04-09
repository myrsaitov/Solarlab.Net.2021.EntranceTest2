using WidePictBoard.Application.Identity.Interfaces;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Application.Services.Content.Implementations;
using Moq;
using MapsterMapper;
using WidePictBoard.Application.MapProfiles;

namespace WidePictBoard.Tests.Content
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
           
            _mapper = new Mapper();
            ContentMapProfile.GetConfiguredMappingConfig().Compile();
            CategoryMapProfile.GetConfiguredMappingConfig().Compile();
            CommentMapProfile.GetConfiguredMappingConfig().Compile();
            ContentMapProfile.GetConfiguredMappingConfig().Compile();
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
