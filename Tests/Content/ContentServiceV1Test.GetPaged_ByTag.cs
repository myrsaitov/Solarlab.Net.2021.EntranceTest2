using WidePictBoard.Application.Services.Content.Contracts;
using WidePictBoard.Application.Services.Content.Contracts.Exceptions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using AutoFixture.Xunit2;
using System.Collections.Generic;
using WidePictBoard.Application.Services.PagedBase.Contracts;
using System;
using System.Linq.Expressions;

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
            ConfigureMoqForGetPagedByTagMethod(
                request,
                userId.ToString(),
                contentTitle,
                contentBody,
                tagBodies,
                categoryId,
                tagSearch);

            // Act
            var response = await _contentServiceV1.GetPaged(
                tagSearch, 
                request, 
                cancellationToken);

            // Assert
            _identityServiceMock.Verify();
            _contentRepositoryMock.Verify();
            _categoryRepositoryMock.Verify();
            _tagRepositoryMock.Verify();
            Assert.NotNull(response);
        }
        [Theory]
        [InlineAutoData(null)]
        public async Task GetPaged_ByTag_Throws_Exception_When_Request_Is_Null(
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
            ConfigureMoqForGetPagedByTagMethod(
                request,
                userId.ToString(),
                contentTitle,
                contentBody,
                tagBodies,
                categoryId,
                tagSearch);

            // Act
            await Assert.ThrowsAsync<ContentGetPagedRequestIsNullException>(
                async () => await _contentServiceV1.GetPaged(
                    tagSearch, 
                    request, 
                    cancellationToken));

        }
        private void ConfigureMoqForGetPagedByTagMethod(
            Paged.Request request,
            string userId,
            string contentTitle,
            string contentBody,
            string[] tagBodies,
            int categoryId,
            string tagSearch)
        {
            int contentCount = 3;

            var responce = new List<Domain.Content>();

            for (int contentId = 1; contentId <= contentCount; contentId++)
            {
                var content = new Domain.Content()
                {
                    Id = contentId,
                    Title = contentTitle,
                    Body = contentBody,
                    OwnerId = userId,
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
        }
    }
}
