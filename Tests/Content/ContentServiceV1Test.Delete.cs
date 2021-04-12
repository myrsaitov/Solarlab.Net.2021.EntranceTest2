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
        public async Task Delete_Returns_Response_Success(
            Delete.Request request, 
            CancellationToken cancellationToken, 
            int userId, 
            string body,
            int contentId, 
            int categoryId,
            int tagId)
        {
            // Arrange
            ConfigureMoqForDeleteMethod(userId.ToString(), body, contentId, categoryId, tagId);

            // Act
            var response = await _contentServiceV1.Delete(request, cancellationToken);

            // Assert
            _identityServiceMock.Verify();
            Assert.NotNull(response);
            Assert.NotEqual(default, response.Id);
        }
        [Theory]
        [InlineAutoData(null)]
        public async Task Delete_Throws_Exception_When_Request_Is_Null(
            Delete.Request request, 
            CancellationToken cancellationToken, 
            int userId, 
            string body,
            int contentId,
            int categoryId,
            int tagId
            )
        {
            // Arrange
            ConfigureMoqForDeleteMethod(userId.ToString(), body, contentId, categoryId, tagId);

            // Act
            await Assert.ThrowsAsync<ContentDeleteRequestIsNullException>(
                async () => await _contentServiceV1.Delete(request, cancellationToken));

        }
        private void ConfigureMoqForDeleteMethod(string userId, string body, int contentId, int categoryId, int tagId)
        {
            var category = new Domain.Category();
            category.Id = categoryId;

            var tag = new Domain.Tag();
            tag.Body = body;

            _identityServiceMock
                .Setup(_ => _.GetCurrentUserId(It.IsAny<CancellationToken>()))
                .ReturnsAsync(userId)
                .Verifiable();
            _categoryRepositoryMock
                .Setup(_ => _.FindById(It.IsAny<int>(), It.IsAny<CancellationToken>())).Returns(async () => category)
                .Callback((int _categoryId, CancellationToken ct) => _categoryId = categoryId);
            _tagRepositoryMock
                .Setup(_ => _.FindWhere(It.IsAny<Expression<Func<Domain.Tag, bool>>>(), It.IsAny<CancellationToken>()))
                .Returns(async () => tag)
                .Callback((Expression<Func<Domain.Tag, bool>> _predicate, CancellationToken ct) => tag.Body = body);
            _tagRepositoryMock
                .Setup(_ => _.Save(It.IsAny<Domain.Tag>(), It.IsAny<CancellationToken>()))
                .Callback((Domain.Tag tag, CancellationToken ct) => tag.Id = tagId);
            _contentRepositoryMock
                .Setup(_ => _.Save(It.IsAny<Domain.Content>(), It.IsAny<CancellationToken>()))
                .Callback((Domain.Content content, CancellationToken ct) => content.Id = contentId);
        }
    }
}
