using WidePictBoard.Application.Services.Content.Contracts;
using WidePictBoard.Application.Services.Content.Contracts.Exceptions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using AutoFixture.Xunit2;
using System.Collections.Generic;
using System;

namespace WidePictBoard.Tests.Content
{
    public partial class ContentServiceV1Test
    {
        [Theory]
        [AutoData]
        public async Task GetById_Returns_Response_Success(
            GetById.Request request, 
            CancellationToken cancellationToken, 
            int userId,
            string contentTitle,
            string contentBody,
            string[] tagBodies,
            int categoryId)
        {
            // Arrange
            var content = new Domain.Content()
            {
                Title = contentTitle,
                Body = contentBody,
                OwnerId = userId.ToString(),
                Category = new Domain.Category()
                {
                    Id = categoryId
                },
                Tags = new List<Domain.Tag>()
            };

            int tagId = 1;
            foreach (string body in tagBodies)
            {
                var tag = new Domain.Tag()
                {
                    Id = tagId++,
                    Body = body
                };
                content.Tags.Add(tag);
            }

            _contentRepositoryMock
                .Setup(_ => _.FindByIdWithUserAndCategoryAndTags(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(content)
                .Callback((int _contentId, CancellationToken ct) => content.Id = _contentId)
                .Verifiable();

            // Act
            var response = await _contentServiceV1.GetById(
                request, 
                cancellationToken);

            // Assert
            _contentRepositoryMock.Verify();
            Assert.NotNull(response);
            Assert.NotEqual(default, response.Id);
        }
        [Theory]
        [AutoData]
        public async Task GetById_Throws_Exception_When_Content_Is_Null(
            GetById.Request request,
            CancellationToken cancellationToken)
        {
            // Act
            await Assert.ThrowsAsync<ContentNotFoundException>(
                async () => await _contentServiceV1.GetById(
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
                async () => await _contentServiceV1.GetById(
                    request, 
                    cancellationToken));
        }
    }
}
