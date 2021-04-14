using WidePictBoard.Application.Services.Comment.Contracts;
using WidePictBoard.Application.Services.Comment.Contracts.Exceptions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using AutoFixture.Xunit2;
using System;

namespace WidePictBoard.Tests.Comment
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
            ConfigureMoqForUpdateMethod(userId.ToString(), commentId);

            // Act
            var response = await _commentServiceV1.Update(request, cancellationToken);

            // Assert
            _identityServiceMock.Verify();
            _commentRepositoryMock.Verify();
            Assert.NotNull(response);
            Assert.NotEqual(default, response.Id);
        }
        [Theory]
        [InlineAutoData(null)]
        public async Task Update_Throws_Exception_When_Request_Is_Null(
            Update.Request request,
            CancellationToken cancellationToken,
            int userId,
            int commentId)
        {
            // Arrange
            ConfigureMoqForUpdateMethod(userId.ToString(), commentId);

            // Act
            await Assert.ThrowsAsync<ArgumentNullException>(
                async () => await _commentServiceV1.Update(request, cancellationToken));

        }
        private void ConfigureMoqForUpdateMethod( 
            string userId, 
            int commentId)
        {
            var comment = new Domain.Comment()
            {
                Id = commentId,
                OwnerId = userId
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
                .ReturnsAsync(userId)
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
        }
    }
}
