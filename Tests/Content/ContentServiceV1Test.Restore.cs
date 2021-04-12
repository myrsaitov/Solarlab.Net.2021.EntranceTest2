using WidePictBoard.Application.Services.Content.Contracts;
using WidePictBoard.Application.Services.Content.Contracts.Exceptions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using AutoFixture.Xunit2;

namespace WidePictBoard.Tests.Content
{
    public partial class ContentServiceV1Test
    {
        [Theory]
        [AutoData]
        public async Task Restore_Returns_Response_Success(
            Restore.Request request, 
            CancellationToken cancellationToken, 
            int userId,
            int contentId)
        {
            // Arrange
            ConfigureMoqForRestoreMethod(userId.ToString(), contentId);

            // Act
            await _contentServiceV1.Restore(request, cancellationToken);

            // Assert
            _identityServiceMock.Verify();
            _contentRepositoryMock.Verify();
        }
        [Theory]
        [InlineAutoData(null)]
        public async Task Restore_Throws_Exception_When_Request_Is_Null(
            Restore.Request request, 
            CancellationToken cancellationToken, 
            int userId, 
            int contentId
            )
        {
            // Arrange
            ConfigureMoqForRestoreMethod(userId.ToString(), contentId);

            // Act
            await Assert.ThrowsAsync<ContentRestoreRequestIsNullException>(
                async () => await _contentServiceV1.Restore(request, cancellationToken));

        }
        private void ConfigureMoqForRestoreMethod(string userId, int contentId)
        {
            var content = new Domain.Content()
            {
                OwnerId = userId
            };

            _contentRepositoryMock
                .Setup(_ => _.FindByIdWithUserInclude(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(content)
                .Callback((int _contentId, CancellationToken ct) => _contentId = contentId)
                .Verifiable();

            _identityServiceMock
                .Setup(_ => _.GetCurrentUserId(It.IsAny<CancellationToken>()))
                .ReturnsAsync(userId)
                .Verifiable();

            _identityServiceMock
                .Setup(_ => _.IsInRole(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false)
                .Verifiable();

            _contentRepositoryMock
                .Setup(_ => _.Save(It.IsAny<Domain.Content>(), It.IsAny<CancellationToken>()))
                .Callback((Domain.Content content, CancellationToken ct) => content.Id = contentId);
        }
    }
}
