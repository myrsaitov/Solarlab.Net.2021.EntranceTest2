using SL2021.Application.Services.Content.Contracts;
using SL2021.Application.Services.Content.Contracts.Exceptions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using AutoFixture.Xunit2;
using System;
using SL2021.Domain.General.Exceptions;

namespace SL2021.Tests.Content
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
            var content = new Domain.Content()
            {
                OwnerId = userId.ToString()
            };

            _contentRepositoryMock
                .Setup(_ => _.FindByIdWithUserInclude(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(content)
                .Callback((int _contentId, CancellationToken ct) => content.Id = _contentId)
                .Verifiable();

            _identityServiceMock
                .Setup(_ => _.GetCurrentUserId(It.IsAny<CancellationToken>()))
                .ReturnsAsync(userId.ToString())
                .Verifiable();

            _identityServiceMock
                .Setup(_ => _.IsInRole(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false)
                .Verifiable();

            _contentRepositoryMock
                .Setup(_ => _.Save(
                    It.IsAny<Domain.Content>(),
                    It.IsAny<CancellationToken>()))
                .Callback((Domain.Content content, CancellationToken ct) => content.Id = contentId);

            // Act
            await _contentServiceV1.Restore(
                request, 
                cancellationToken);

            // Assert
            _identityServiceMock.Verify();
            _contentRepositoryMock.Verify();
        }
        [Theory]
        [AutoData]
        public async Task Restore_Throws_Exception_When_No_Rights(
            Restore.Request request,
            CancellationToken cancellationToken,
            int userId)
        {
            // Arrange
            var content = new Domain.Content()
            {
                OwnerId = userId.ToString()
            };

            _contentRepositoryMock
                .Setup(_ => _.FindByIdWithUserInclude(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(content)
                .Callback((int _contentId, CancellationToken ct) => content.Id = _contentId);

            _identityServiceMock
                .Setup(_ => _.GetCurrentUserId(It.IsAny<CancellationToken>()))
                .ReturnsAsync("");

            _identityServiceMock
                .Setup(_ => _.IsInRole(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            await Assert.ThrowsAsync<NoRightsException>(
                async () => await _contentServiceV1.Restore(
                    request,
                    cancellationToken));
        }
        [Theory]
        [AutoData]
        public async Task Restore_Throws_Exception_When_Content_Is_Null(
            Restore.Request request,
            CancellationToken cancellationToken)
        {
            // Act
            await Assert.ThrowsAsync<ContentNotFoundException>(
                async () => await _contentServiceV1.Restore(
                    request,
                    cancellationToken));
        }
        [Theory]
        [InlineAutoData(null)]
        public async Task Restore_Throws_Exception_When_Request_Is_Null(
            Restore.Request request, 
            CancellationToken cancellationToken
            )
        {
            // Act
            await Assert.ThrowsAsync<ArgumentNullException>(
                async () => await _contentServiceV1.Restore(
                    request, 
                    cancellationToken));
        }
    }
}
