using WidePictBoard.Application.Services.Content.Contracts;
using WidePictBoard.Application.Services.Content.Contracts.Exceptions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using AutoFixture.Xunit2;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;
using WidePictBoard.Application.Services;
using System.Linq;

namespace WidePictBoard.Tests.Content
{
    public partial class ContentServiceV1Test
    {
        [Theory]
        [AutoData]
        public async Task GetPaged_ByTag_Returns_Response_Success(
            Paged.Request request,
            CancellationToken cancellationToken,
            int userId,
            string contentTitle,
            string contentBody,
            string[] tagBodies,
            int categoryId,
            string tagSearch)
        {
            // Arrange
            int contentCount = 3;

            var responce = new List<Domain.Content>();

            for (int contentId = 1; contentId <= contentCount; contentId++)
            {
                var content = new Domain.Content()
                {
                    Id = contentId,
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

                content.Tags.Add(new Domain.Tag()
                {
                    Id = tagId,
                    Body = tagSearch
                });

                responce.Add(content);
            }

            _contentRepositoryMock
                .Setup(_ => _.Count(
                    It.IsAny<Expression<Func<Domain.Content, bool>>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(contentCount)
                .Verifiable();

            _contentRepositoryMock
                .Setup(_ => _.GetPagedWithTagsInclude(
                    It.IsAny<string>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(responce)
                .Verifiable();

            // Act
            var response = await _contentServiceV1.GetPaged(
                tagSearch, 
                request, 
                cancellationToken);

            // Assert
            _contentRepositoryMock.Verify();
            Assert.NotNull(response);
            Assert.Equal(contentCount, response.Total);
            Assert.Equal(contentCount, response.Items.Count());
            Assert.IsType<Paged.Response<GetById.Response>>(response);
        }
        [Theory]
        [AutoData]
        public async Task GetPaged_ByTag_Returns_Response_Success_Total_eq_0(
            Paged.Request request,
            CancellationToken cancellationToken,
            string tagSearch)
        {
            // Arrange
            int contentCount = 0;

            var responce = new List<Domain.Content>();

            _contentRepositoryMock
                .Setup(_ => _.Count(
                    It.IsAny<Expression<Func<Domain.Content, bool>>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(contentCount)
                .Verifiable();

            // Act
            var response = await _contentServiceV1.GetPaged(
                tagSearch,
                request,
                cancellationToken);

            // Assert
            _contentRepositoryMock.Verify();
            Assert.NotNull(response);
            Assert.Equal(contentCount, response.Total);
            Assert.Equal(contentCount, response.Items.Count());
            Assert.IsType<Paged.Response<GetById.Response>>(response);
        }
        [Theory]
        [InlineAutoData(null)]
        public async Task GetPaged_ByTag_Throws_Exception_When_Request_Is_Null(
            Paged.Request request,
            CancellationToken cancellationToken,
            string tagSearch)
        {
            // Act
            await Assert.ThrowsAsync<ContentGetPagedRequestIsNullException>(
                async () => await _contentServiceV1.GetPaged(
                    tagSearch, 
                    request, 
                    cancellationToken));
        }
    }
}
