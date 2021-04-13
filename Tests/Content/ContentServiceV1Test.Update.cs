using WidePictBoard.Application.Services.Content.Contracts;
using WidePictBoard.Application.Services.Content.Contracts.Exceptions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using AutoFixture.Xunit2;
using System.Linq.Expressions;
using System;

namespace WidePictBoard.Tests.Content
{
    public partial class ContentServiceV1Test
    {
        [Theory]
        [AutoData]
        public async Task Update_Returns_Response_Success(
            Update.Request request,
            CancellationToken cancellationToken,
            int userId,
            int contentId,
            int categoryId)
        {
            // Arrange
            ConfigureMoqForUpdateMethod(
                request, 
                userId.ToString(), 
                contentId, 
                categoryId);

            // Act
            var response = await _contentServiceV1.Update(
                request, 
                cancellationToken);

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
        public async Task Update_Throws_Exception_When_Request_Is_Null(
            Update.Request request,
            CancellationToken cancellationToken,
            int userId,
            int contentId,
            int categoryId
            )
        {
            // Arrange
            ConfigureMoqForUpdateMethod(
                request, 
                userId.ToString(), 
                contentId, 
                categoryId);

            // Act
            await Assert.ThrowsAsync<ContentUpdateRequestIsNullException>(
                async () => await _contentServiceV1.Update(
                    request, 
                    cancellationToken));

        }
        private void ConfigureMoqForUpdateMethod(
            Update.Request request, 
            string userId, 
            int contentId, 
            int categoryId)
        {
            var category = new Domain.Category()
            {
                Id = categoryId
            };

            var content = new Domain.Content()
            {
                Id = contentId,
                OwnerId = userId,
                CategoryId = categoryId
            };

            int tagId = 1;

            _contentRepositoryMock
                .Setup(_ => _.FindByIdWithUserAndCategoryAndTags(
                    It.IsAny<int>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(content)
                .Callback((int _contentId, CancellationToken ct) => content.Id = _contentId)
                .Verifiable();

            _identityServiceMock
                .Setup(_ => _.GetCurrentUserId(It.IsAny<CancellationToken>()))
                .ReturnsAsync(userId)
                .Verifiable();

            _categoryRepositoryMock
                .Setup(_ => _.FindById(
                    It.IsAny<int>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(category)
                .Callback((int _categoryId, CancellationToken ct) => category.Id = _categoryId)
                .Verifiable();

            _identityServiceMock
                .Setup(_ => _.IsInRole(
                    It.IsAny<string>(), 
                    It.IsAny<string>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false)
                .Verifiable();

            _categoryRepositoryMock
                .Setup(_ => _.FindById(
                    It.IsAny<int>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(category)
                .Callback(() => category.Id = categoryId)
                .Verifiable();

            _tagRepositoryMock
                .Setup(_ => _.FindWhere(
                    It.IsAny<Expression<Func<Domain.Tag, bool>>>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => new Domain.Tag()
                {
                    Id = tagId,
                    Body = request.TagBodies[tagId++ - 1]
                })
                .Verifiable();

            _tagRepositoryMock
                .Setup(_ => _.Save(
                    It.IsAny<Domain.Tag>(), 
                    It.IsAny<CancellationToken>()));

            _contentRepositoryMock
                .Setup(_ => _.Save(
                    It.IsAny<Domain.Content>(), 
                    It.IsAny<CancellationToken>()))
                .Callback((Domain.Content content, CancellationToken ct) => content.Id = contentId);
        }
    }
}
