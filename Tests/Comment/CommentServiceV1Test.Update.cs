using SL2021.Application.Services.Comment.Contracts;
using SL2021.Application.Services.Comment.Contracts.Exceptions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using AutoFixture.Xunit2;
using System;
using SL2021.Domain.General.Exceptions;

namespace SL2021.Tests.Comment
{
    public partial class CommentServiceV1Test
    {
        [Theory]
        [AutoData]
        public async Task Update_Returns_Response_Success(
            Update.Request request,
            CancellationToken cancellationToken,
            int userId,
            int commentId)
        {
            // Arrange
            var comment = new Domain.Comment()
            {
                Id = commentId,
                OwnerId = userId.ToString()
            };

            _commentRepositoryMock
                .Setup(_ => _.FindByIdWithUserInclude(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(comment)
                .Callback((int _commentId, CancellationToken ct) => comment.Id = commentId)
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

            _identityServiceMock
                .Setup(_ => _.IsInRole(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false)
                .Verifiable();

            _commentRepositoryMock
                .Setup(_ => _.Save(
                    It.IsAny<Domain.Comment>(),
                    It.IsAny<CancellationToken>()))
                .Callback((Domain.Comment comment, CancellationToken ct) => comment.Id = commentId);

            // Act
            var response = await _commentServiceV1.Update(
                request, 
                cancellationToken);

            // Assert
            _identityServiceMock.Verify();
            _commentRepositoryMock.Verify();
            Assert.NotNull(response);
            Assert.NotEqual(default, response.Id);
        }
        [Theory]
        [AutoData]
        public async Task Update_Throws_Exception_When_No_Rights(
            Update.Request request,
            CancellationToken cancellationToken,
            int userId,
            int commentId)
        {
            // Arrange
            var comment = new Domain.Comment()
            {
                Id = commentId,
                OwnerId = userId.ToString()
            };

            _commentRepositoryMock
                .Setup(_ => _.FindByIdWithUserInclude(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(comment)
                .Callback((int _commentId, CancellationToken ct) => comment.Id = commentId);

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
                async () => await _commentServiceV1.Update(
                    request,
                    cancellationToken));
        }
        [Theory]
        [AutoData]
        public async Task Update_Throws_Exception_When_Comment_Is_Null(
            Update.Request request,
            CancellationToken cancellationToken)
        {
            // Act
            await Assert.ThrowsAsync<CommentNotFoundException>(
                async () => await _commentServiceV1.Update(
                    request,
                    cancellationToken));
        }
        [Theory]
        [InlineAutoData(null)]
        public async Task Update_Throws_Exception_When_Request_Is_Null(
            Update.Request request,
            CancellationToken cancellationToken)
        {
            // Act
            await Assert.ThrowsAsync<ArgumentNullException>(
                async () => await _commentServiceV1.Update(
                    request, 
                    cancellationToken));
        }
    }
}
