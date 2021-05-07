using SL2021.Application.Services.Content.Contracts;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using AutoFixture.Xunit2;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;
using SL2021.Application.Services.Contracts;
using System.Linq;

namespace SL2021.Tests.Content
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
                .Setup(_ => _.GetPagedWithTagsAndOwnerAndCategoryInclude(
                    It.IsAny<Expression<Func<Domain.Content, bool>>>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(responce)
                .Verifiable();

            // Act
            var response = await _contentServiceV1.GetPaged(
                a => a.Tags.Any(t => t.Body == tagSearch), 
                request, 
                cancellationToken);

            // Assert
            _contentRepositoryMock.Verify();
            Assert.NotNull(response);
            Assert.Equal(contentCount, response.Total);
            Assert.Equal(contentCount, response.Items.Count());
            Assert.IsType<Paged.Response<GetPaged.Response>>(response);
        }
        [Theory]
        [AutoData]
        public async Task GetPaged_ByTag_Returns_Response_Success_Total_eq_0(
            Paged.Request request,
            CancellationToken cancellationToken,
            Expression<Func<Domain.Content, bool>> predicate)
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
                predicate,
                request,
                cancellationToken);

            // Assert
            _contentRepositoryMock.Verify();
            Assert.NotNull(response);
            Assert.Equal(contentCount, response.Total);
            Assert.Equal(contentCount, response.Items.Count());
            Assert.IsType<Paged.Response<GetPaged.Response>>(response);
        }
        [Theory]
        [InlineAutoData(null)]
        public async Task GetPaged_ByTag_Throws_Exception_When_Request_Is_Null(
            Paged.Request request,
            CancellationToken cancellationToken,
            Expression<Func<Domain.Content, bool>> predicate)
        {
            // Act
            await Assert.ThrowsAsync<ArgumentNullException>(
                async () => await _contentServiceV1.GetPaged(
                    predicate, 
                    request, 
                    cancellationToken));
        }
    }
}
