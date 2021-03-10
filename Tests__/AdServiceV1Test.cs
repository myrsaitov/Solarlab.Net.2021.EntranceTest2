using Advertisement.Application.Identity.Interfaces;
using Advertisement.Application.Repositories;
using Advertisement.Application.Services.Ad.Contracts;
using Advertisement.Application.Services.Ad.Implementations;
using AutoFixture.Xunit2;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Advertisement.Application.Tests
{
    public class AdServiceV1Test
    {
        private Mock<IAdRepository> _adRepositoryMock;
        private Mock<IIdentityService> _identityServiceMock;

        private AdServiceV1 _adServiceV1;
        public AdServiceV1Test()
        {
            _adRepositoryMock = new Mock<IAdRepository>();
            _identityServiceMock = new Mock<IIdentityService>();

            _adServiceV1 = new AdServiceV1(_adRepositoryMock.Object, _identityServiceMock.Object);
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
                .Setup(_ => _.Save(It.IsAny<Domain.Ad>(), It.IsAny<CancellationToken>()))
                .Callback((Domain.Ad ad, CancellationToken ct) => ad.Id = adId);
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
