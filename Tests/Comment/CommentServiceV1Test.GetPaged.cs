using WidePictBoard.Application.Services.Comment.Contracts.Exceptions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using AutoFixture.Xunit2;
using System.Collections.Generic;
using WidePictBoard.Application.Services.PagedBase.Contracts;
using System.Linq.Expressions;
using System;

namespace WidePictBoard.Tests.Comment
{
    public partial class CommentServiceV1Test
    {
        [Theory]
        [AutoData]
        public async Task GetPaged_Returns_Response_Success(
            Paged.Request request, 
            CancellationToken cancellationToken, 
            int userId,
            int contentId)
        {
            // Arrange
            ConfigureMoqForGetPagedMethod(userId.ToString());

            // Act
            var response = await _commentServiceV1.GetPaged(
                contentId, 
                request, 
                cancellationToken);

            // Assert
            _identityServiceMock.Verify();
            _commentRepositoryMock.Verify();
            Assert.NotNull(response);
        }
        [Theory]
        [InlineAutoData(null)]
        public async Task GetPaged_Throws_Exception_When_Request_Is_Null(
            Paged.Request request, 
            CancellationToken cancellationToken, 
            int userId,
            int contentId)
        {
            // Arrange
            ConfigureMoqForGetPagedMethod(userId.ToString());

            // Act
            await Assert.ThrowsAsync<CommentGetPagedRequestIsNullException>(
                async () => await _commentServiceV1.GetPaged(
                    contentId, 
                    request, 
                    cancellationToken));

        }
        private void ConfigureMoqForGetPagedMethod(string userId)
        {
            int commentCount = 3;

            var responce = new List<Domain.Comment>();

            for (int commentId = 1; commentId <= commentCount; commentId++)
            {
                var comment = new Domain.Comment()
                {
                    Id = commentId,
                    OwnerId = userId
                };
                responce.Add(comment);
            }

            var content = new Domain.Content();

            _commentRepositoryMock
                .Setup(_ => _.Count(
                    It.IsAny<Expression<Func<Domain.Comment, bool>>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(commentCount)
                .Verifiable();

            _contentRepositoryMock
                .Setup(_ => _.FindById(
                    It.IsAny<int>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(content)
                .Callback((int _contentId, CancellationToken ct) => content.Id = _contentId)
                .Verifiable();

            _commentRepositoryMock
                .Setup(_ => _.GetPaged(
                    It.IsAny<Expression<Func<Domain.Comment, bool>>>(), 
                    It.IsAny<int>(), 
                    It.IsAny<int>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(responce)
                .Verifiable();
        }
    }
}
