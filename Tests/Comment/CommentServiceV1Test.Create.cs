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
        public async Task Create_Returns_Response_Success(
            Create.Request request, 
            CancellationToken cancellationToken, 
            int userId, 
            int commentId)
        {
            // Arrange
            ConfigureMoqForCreateMethod(
                request, 
                userId.ToString(), 
                commentId);

            // Act
            var response = await _commentServiceV1.Create(
                request, 
                cancellationToken);

            // Assert
            _identityServiceMock.Verify();
            _commentRepositoryMock.Verify();
            Assert.NotNull(response);
            Assert.NotEqual(default, response.Id);
        }
        [Theory]
        [InlineAutoData(null)]
        public async Task Create_Throws_Exception_When_Request_Is_Null(
            Create.Request request, 
            CancellationToken cancellationToken, 
            int userId, 
            int commentId
            )
        {
            // Arrange
            ConfigureMoqForCreateMethod(
                request, 
                userId.ToString(), 
                commentId);

            // Act
            await Assert.ThrowsAsync<CommentCreateRequestIsNullException>(
                async () => await _commentServiceV1.Create(
                    request, 
                    cancellationToken));

        }
        private void ConfigureMoqForCreateMethod(
            Create.Request request,
            string userId, 
            int commentId)
        {
            var content = new Domain.Content();

            var comment = new Domain.Comment()
            {
                OwnerId = userId
            };

            _identityServiceMock
                .Setup(_ => _.GetCurrentUserId(It.IsAny<CancellationToken>()))
                .ReturnsAsync(userId)
                .Verifiable();

            _contentRepositoryMock
                .Setup(_ => _.FindByIdWithUserInclude(
                    It.IsAny<int>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(content)
                .Callback((int _contentId, CancellationToken ct) => content.Id = _contentId)
                .Verifiable();

            _commentRepositoryMock
                .Setup(_ => _.FindByIdWithUserAndCommentsInclude(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(comment)
                .Callback((int _commentId, CancellationToken ct) => comment.Id = (int)request.ParentCommentId)
                .Verifiable();

            _commentRepositoryMock
                .Setup(_ => _.Save(
                    It.IsAny<Domain.Comment>(), 
                    It.IsAny<CancellationToken>()))
                .Callback((Domain.Comment comment, CancellationToken ct) => comment.Id = commentId);
        }
    }
}
