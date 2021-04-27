using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using AutoFixture.Xunit2;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using SL2021.Application.Services.Contracts;
using SL2021.Application.Services.Content.Contracts.Exceptions;

namespace SL2021.Tests.Comment
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
            int commentCount = 3;

            var responce = new List<Domain.Comment>();

            for (int commentId = 1; commentId <= commentCount; commentId++)
            {
                responce.Add(new Domain.Comment()
                {
                    Id = commentId,
                    OwnerId = userId.ToString()
                });
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
                .Setup(_ => _.GetPagedWithOwnerInclude(
                    It.IsAny<Expression<Func<Domain.Comment, bool>>>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(responce)
                .Verifiable();

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
        [AutoData]
        public async Task GetPaged_Returns_Response_Success_Total_eq_0(
            Paged.Request request,
            CancellationToken cancellationToken,
            int contentId)
        {
            // Arrange
            int commentCount = 0;

            var responce = new List<Domain.Comment>();
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

            // Act
            var response = await _commentServiceV1.GetPaged(
                contentId,
                request,
                cancellationToken);

            // Assert
            _commentRepositoryMock.Verify();
            _contentRepositoryMock.Verify();
            Assert.NotNull(response);
        }
        [Theory]
        [InlineAutoData(null)]
        public async Task GetPaged_Throws_Exception_When_Request_Is_Null(
            Paged.Request request, 
            CancellationToken cancellationToken, 
            int contentId)
        {
            // Act
            await Assert.ThrowsAsync<ArgumentNullException>(
                async () => await _commentServiceV1.GetPaged(
                    contentId, 
                    request, 
                    cancellationToken));
        }
        [Theory]
        [AutoData]
        public async Task GetPaged_Throws_Exception_When_Content_NotFound(
            Paged.Request request,
            CancellationToken cancellationToken,
            int contentId)
        {
            // Act
            await Assert.ThrowsAsync<ContentNotFoundException>(
                async () => await _commentServiceV1.GetPaged(
                    contentId,
                    request,
                    cancellationToken));
        }
    }
}
