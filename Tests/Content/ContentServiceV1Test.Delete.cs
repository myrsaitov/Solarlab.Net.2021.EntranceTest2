using WidePictBoard.Application.Services.Content.Contracts;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using AutoFixture.Xunit2;
using System;
using WidePictBoard.Application.Services.Content.Contracts.Exceptions;
using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Tests.Content
{
    public partial class ContentServiceV1Test
    {
        [Theory]
        [AutoData]
        public async Task Delete_Returns_Response_Success(
            Delete.Request request, 
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
                .Setup(_ => _.GetCurrentUserId(
                    It.IsAny<CancellationToken>()))
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
            await _contentServiceV1.Delete(
                request, 
                cancellationToken);

            // Assert
            _identityServiceMock.Verify();
            _contentRepositoryMock.Verify();
        }
        [Theory]
        [AutoData]
        public async Task Delete_Throws_Exception_When_No_Rights(
    Delete.Request request,
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
                .Setup(_ => _.FindByIdWithUserInclude(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(content)
                .Callback((int _contentId, CancellationToken ct) => content.Id = _contentId)
                .Verifiable();

            _identityServiceMock
                .Setup(_ => _.GetCurrentUserId(It.IsAny<CancellationToken>()))
                .ReturnsAsync("")
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
            await Assert.ThrowsAsync<NoRightsException>(
                async () => await _contentServiceV1.Delete(
                    request, 
                    cancellationToken));
        }
        [Theory]
        [AutoData]
        public async Task Delete_Throws_Exception_When_Content_Is_Null(
            Delete.Request request,
            CancellationToken cancellationToken)
        {
            // Act
            await Assert.ThrowsAsync<ContentNotFoundException>(
                async () => await _contentServiceV1.Delete(
                    request, 
                    cancellationToken));
        }
        [Theory]
        [InlineAutoData(null)]
        public async Task Delete_Throws_Exception_When_Request_Is_Null(
            Delete.Request request, 
            CancellationToken cancellationToken)
        {
            // Act
            await Assert.ThrowsAsync<ArgumentNullException>(
                async () => await _contentServiceV1.Delete(
                    request, 
                    cancellationToken));
        }
    }
}
