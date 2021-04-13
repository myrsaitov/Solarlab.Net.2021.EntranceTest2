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
        public async Task GetById_Returns_Response_Success(
            GetById.Request request, 
            CancellationToken cancellationToken, 
            int userId,
            string commentBody)
        {
            // Arrange
            ConfigureMoqForGetByIdMethod(
                userId.ToString(), 
                commentBody);

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
        [InlineAutoData(null)]
        public async Task GetById_Throws_Exception_When_Request_Is_Null(
            GetById.Request request, 
            CancellationToken cancellationToken, 
            int userId,
            string commentBody)
        {
            // Arrange
            ConfigureMoqForGetByIdMethod(
                userId.ToString(), 
                commentBody);

            // Act
            await Assert.ThrowsAsync<CommentGetByIdRequestIsNullException>(
                async () => await _commentServiceV1.GetById(
                    request, 
                    cancellationToken));

        }
        private void ConfigureMoqForGetByIdMethod(
            string userId,
            string commentBody)
        {
            var comment = new Domain.Comment()
            {
                Body = commentBody,
                OwnerId = userId
            };

            _commentRepositoryMock
                .Setup(_ => _.FindByIdWithUserAndCommentsInclude(
                    It.IsAny<int>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(comment)
                .Callback((int _commentId, CancellationToken ct) => comment.Id = _commentId)
                .Verifiable();
        }
    }
}
