using WidePictBoard.Application.Services.Content.Contracts.Exceptions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using AutoFixture.Xunit2;
using System.Collections.Generic;
using WidePictBoard.Application.Services;

namespace WidePictBoard.Tests.Content
{
    public partial class ContentServiceV1Test
    {
        [Theory]
        [AutoData]
        public async Task GetPaged_Returns_Response_Success(
            Paged.Request request, 
            CancellationToken cancellationToken, 
            int userId,
            string contentTitle,
            string contentBody,
            string[] tagBodies,
            int categoryId)
        {
            // Arrange
            ConfigureMoqForGetPagedMethod(
                request,
                userId.ToString(),
                contentTitle,
                contentBody,
                tagBodies,
                categoryId);

            // Act
            var response = await _contentServiceV1.GetPaged(
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
        public async Task GetPaged_Throws_Exception_When_Request_Is_Null(
            Paged.Request request, 
            CancellationToken cancellationToken, 
            int userId,
            string contentTitle,
            string contentBody,
            string[] tagBodies,
            int categoryId)
        {
            // Arrange
            ConfigureMoqForGetPagedMethod(
                request,
                userId.ToString(),
                contentTitle,
                contentBody,
                tagBodies,
                categoryId);

            // Act
            await Assert.ThrowsAsync<ContentGetPagedRequestIsNullException>(
                async () => await _contentServiceV1.GetPaged(
                    request, 
                    cancellationToken));

        }
        private void ConfigureMoqForGetPagedMethod(
            Paged.Request request,
            string userId,
            string contentTitle,
            string contentBody,
            string[] tagBodies,
            int categoryId)
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
                responce.Add(content);
            }
            
            _contentRepositoryMock
                .Setup(_ => _.Count(It.IsAny<CancellationToken>()))
                .ReturnsAsync(contentCount)
                .Verifiable();

            _contentRepositoryMock
                .Setup(_ => _.GetPagedWithTagsInclude(
                    It.IsAny<int>(), 
                    It.IsAny<int>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(responce)
                .Verifiable();
        }
    }
}
