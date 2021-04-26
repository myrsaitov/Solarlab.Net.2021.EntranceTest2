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
        public async Task GetById_Returns_Response_Success(
            GetById.Request request, 
            CancellationToken cancellationToken, 
            int userId,
            string commentBody)
        {
            // Arrange
            var comment = new Domain.Comment()
            {
                Body = commentBody,
                OwnerId = userId.ToString()
            };

            _commentRepositoryMock
                .Setup(_ => _.FindByIdWithUserAndCommentsInclude(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(comment)
                .Callback((int _commentId, CancellationToken ct) => comment.Id = _commentId)
                .Verifiable();

            // Act
            var response = await _commentServiceV1.GetById(
                request, 
                cancellationToken);

            // Assert
            _commentRepositoryMock.Verify();
            Assert.NotNull(response);
            Assert.NotEqual(default, response.Id);
        }
        [Theory]
        [AutoData]
        public async Task GetById_Throws_Exception_When_Comment_Is_Null(
            GetById.Request request,
            CancellationToken cancellationToken)
        {
            // Act
            await Assert.ThrowsAsync<CommentNotFoundException>(
                async () => await _commentServiceV1.GetById(
                    request,
                    cancellationToken));
        }
        [Theory]
        [InlineAutoData(null)]
        public async Task GetById_Throws_Exception_When_Request_Is_Null(
            GetById.Request request, 
            CancellationToken cancellationToken)
        {
            // Act
            await Assert.ThrowsAsync<ArgumentNullException>(
                async () => await _commentServiceV1.GetById(
                    request, 
                    cancellationToken));
        }
    }
}
