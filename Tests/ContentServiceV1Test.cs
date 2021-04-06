using WidePictBoard.Application.Identity.Interfaces;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Application.Services.Content.Contracts;
using WidePictBoard.Application.Services.Content.Implementations;
using AutoFixture.Xunit2;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using MapsterMapper;
using WidePictBoard.Application.MapProfiles;

namespace WidePictBoard.Tests
{
    public class ContentServiceV1Test
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

        [Theory]
        [AutoData]
        public async Task Create_Returns_Response_Success(
            Create.Request request, CancellationToken cancellationToken, int userId, int contentId, int categoryId)
        {
            // Arrange
            ConfigureMoqForCreateMethod(userId.ToString(), contentId, categoryId);

            // Act
            var response = await _contentServiceV1.Create(request, cancellationToken);

            // Assert
            _identityServiceMock.Verify();
            Assert.NotNull(response);
            Assert.NotEqual(default, response.Id);
        }

        private void ConfigureMoqForCreateMethod(string userId, int contentId, int categoryId)
        {
            var category = new Domain.Category();
            category.Id = categoryId;

            _identityServiceMock
                .Setup(_ => _.GetCurrentUserId(It.IsAny<CancellationToken>()))
                .ReturnsAsync(userId)
                .Verifiable();
            _contentRepositoryMock
                .Setup(_ => _.Save(It.IsAny<Domain.Content>(), It.IsAny<CancellationToken>()))
                .Callback((Domain.Content content, CancellationToken ct) => content.Id = contentId);
            _ = _categoryRepositoryMock
                .Setup(_ => _.FindById(It.IsAny<int>(), It.IsAny<CancellationToken>())).Returns(async () => category)
                .Callback((int _categoryId, CancellationToken ct) => _categoryId = categoryId);
        }
    }
}
