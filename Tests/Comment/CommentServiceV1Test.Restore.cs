using WidePictBoard.Application.Services.Comment.Contracts;
using WidePictBoard.Application.Services.Comment.Contracts.Exceptions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using AutoFixture.Xunit2;

namespace WidePictBoard.Tests.Comment
{
    public partial class CommentServiceV1Test
    {
        [Theory]
        [AutoData]
        public async Task Restore_Returns_Response_Success(
            Restore.Request request, 
            CancellationToken cancellationToken, 
            int userId,
            int commentId)
        {
            // Arrange
            ConfigureMoqForRestoreMethod(
                userId.ToString(), 
                commentId);

            // Act
            await _commentServiceV1.Restore(
                request, 
                cancellationToken);

            // Assert
            _identityServiceMock.Verify();
            _commentRepositoryMock.Verify();
        }
        [Theory]
        [InlineAutoData(null)]
        public async Task Restore_Throws_Exception_When_Request_Is_Null(
            Restore.Request request, 
            CancellationToken cancellationToken, 
            int userId, 
            int commentId
            )
        {
            // Arrange
            ConfigureMoqForRestoreMethod(
                userId.ToString(), 
                commentId);

            // Act
            await Assert.ThrowsAsync<CommentRestoreRequestIsNullException>(
                async () => await _commentServiceV1.Restore(request, cancellationToken));

        }
        private void ConfigureMoqForRestoreMethod(
            string userId, 
            int commentId)
        {
            var comment = new Domain.Comment()
            {
                OwnerId = userId
            };

            _commentRepositoryMock
                .Setup(_ => _.FindByIdWithUserInclude(
                    It.IsAny<int>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(comment)
                .Callback((int _commentId, CancellationToken ct) => comment.Id = _commentId)
                .Verifiable();

            _identityServiceMock
                .Setup(_ => _.GetCurrentUserId(It.IsAny<CancellationToken>()))
                .ReturnsAsync(userId)
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
        }
    }
}
