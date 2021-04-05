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

namespace WidePictBoard.Tests
{
    public class ContentServiceV1Test
    {
        private Mock<IContentRepository> _contentRepositoryMock;
        private Mock<ICategoryRepository> _categoryRepositoryMock;
        private Mock<ITagRepository> _tagRepositoryMock;
        private Mock<IIdentityService> _identityServiceMock;
        private Mock<IMapper> _mapperMock;
        
        private ContentServiceV1 _contentServiceV1;
        public ContentServiceV1Test()
        {
            _contentRepositoryMock = new Mock<IContentRepository>();
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _tagRepositoryMock = new Mock<ITagRepository>();
            _identityServiceMock = new Mock<IIdentityService>();
            _mapperMock = new Mock<IMapper>();

            _contentServiceV1 = new ContentServiceV1(
                _contentRepositoryMock.Object,
                _categoryRepositoryMock.Object, 
                _tagRepositoryMock.Object, 
                _identityServiceMock.Object,
                _mapperMock.Object);
        }

        [Theory]
        [AutoData]
        public async Task Create_Returns_Response_Success(
            Create.Request request, CancellationToken cancellationToken, int userId, int contentId)
        {
            // Arrange
            ConfigureMoqForCreateMethod(userId.ToString(), contentId);

            // Act
            var response = await _contentServiceV1.Create(request, cancellationToken);

            // Assert
            _identityServiceMock.Verify();
            Assert.NotNull(response);
            Assert.NotEqual(default, response.Id);
        }

        private void ConfigureMoqForCreateMethod(string userId, int contentId)
        {
            _identityServiceMock
                .Setup(_ => _.GetCurrentUserId(It.IsAny<CancellationToken>()))
                .ReturnsAsync(userId)
                .Verifiable();
            _contentRepositoryMock
                .Setup(_ => _.Save(It.IsAny<Domain.Content>(), It.IsAny<CancellationToken>()))
                .Callback((Domain.Content content, CancellationToken ct) => content.Id = contentId);
        }

        [Theory]
        [InlineAutoData(null)]
        public async Task Create_Throws_Exception_When_Request_Is_Null(
            Create.Request request, CancellationToken cancellationToken, int userId, int contentId)
        {
            ConfigureMoqForCreateMethod(userId.ToString(), contentId);

            // Assert
            _identityServiceMock.Verify();
        }
    }
}
