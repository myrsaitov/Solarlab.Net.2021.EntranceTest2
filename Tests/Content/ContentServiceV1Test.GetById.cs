using WidePictBoard.Application.Services.Content.Contracts;
using WidePictBoard.Application.Services.Content.Contracts.Exceptions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using AutoFixture.Xunit2;
using System.Collections.Generic;

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
            ConfigureMoqForGetByIdMethod(
                userId.ToString(), 
                contentTitle, 
                contentBody, 
                tagBodies, 
                categoryId);

            // Act
            var response = await _contentServiceV1.GetById(request, cancellationToken);

            // Assert
            _identityServiceMock.Verify();
            _contentRepositoryMock.Verify();
            _categoryRepositoryMock.Verify();
            _tagRepositoryMock.Verify();
            Assert.NotNull(response);
            Assert.NotEqual(default, response.Id);
        }
        [Theory]
        [InlineAutoData(null)]
        public async Task GetById_Throws_Exception_When_Request_Is_Null(
            GetById.Request request, 
            CancellationToken cancellationToken, 
            int userId,
            string contentTitle,
            string contentBody,
            string[] tagBodies,
            int categoryId)
        {
            // Arrange
            ConfigureMoqForGetByIdMethod(userId.ToString(), contentTitle, contentBody, tagBodies, categoryId);

            // Act
            await Assert.ThrowsAsync<ContentGetByIdRequestIsNullException>(
                async () => await _contentServiceV1.GetById(request, cancellationToken));

        }
        private void ConfigureMoqForGetByIdMethod(
            string userId, 
            string contentTitle, 
            string contentBody, 
            string[] tagBodies, 
            int categoryId)
        {
            var content = new Domain.Content()
            {
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

            _contentRepositoryMock
                .Setup(_ => _.FindByIdWithUserAndCategoryAndTags(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(content)
                .Callback((int _contentId, CancellationToken ct) => content.Id = _contentId)
                .Verifiable();
        }
    }
}
