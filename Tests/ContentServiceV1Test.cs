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
        private Mock<IContentRepository> _adRepositoryMock;
        private Mock<IIdentityService> _identityServiceMock;
        private IMapper _mapper;

        private ContentServiceV1 _adServiceV1;
        public ContentServiceV1Test()
        {
            _adRepositoryMock = new Mock<IContentRepository>();
            _identityServiceMock = new Mock<IIdentityService>();

            _adServiceV1 = new ContentServiceV1(_adRepositoryMock.Object, _identityServiceMock.Object, _mapper);
        }

        [Theory]
        [AutoData]
        public async Task Create_Returns_Response_Success(
            Create.Request request, CancellationToken cancellationToken, int userId, int adId)
        {
            // Arrange
            ConfigureMoqForCreateMethod(userId.ToString(), adId);

            // Act
            var response = await _adServiceV1.Create(request, cancellationToken);

            // Assert
            _identityServiceMock.Verify();
            Assert.NotNull(response);
            Assert.NotEqual(default, response.Id);
        }

        private void ConfigureMoqForCreateMethod(string userId, int adId)
        {
            _identityServiceMock
                .Setup(_ => _.GetCurrentUserId(It.IsAny<CancellationToken>()))
                .ReturnsAsync(userId)
                .Verifiable();
            _adRepositoryMock
                .Setup(_ => _.Save(It.IsAny<Domain.Content>(), It.IsAny<CancellationToken>()))
                .Callback((Domain.Content content, CancellationToken ct) => content.Id = adId);
        }

        [Theory]
        [InlineAutoData(null)]
        public async Task Create_Throws_Exception_When_Request_Is_Null(
            Create.Request request, CancellationToken cancellationToken, int userId, int adId)
        {
            ConfigureMoqForCreateMethod(userId.ToString(), adId);

            // Assert
            _identityServiceMock.Verify();
        }
    }
}
