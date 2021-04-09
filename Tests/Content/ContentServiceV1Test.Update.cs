using WidePictBoard.Application.Services.Content.Contracts;
using WidePictBoard.Application.Services.Content.Contracts.Exceptions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using AutoFixture.Xunit2;

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
            ConfigureMoqForUpdateMethod(userId.ToString(), contentId, categoryId);

            // Act
            var response = await _contentServiceV1.Update(request, cancellationToken);

            // Assert
            _identityServiceMock.Verify();
            Assert.NotNull(response);
            Assert.NotEqual(default, response.Id);
        }
        [Theory]
        [InlineAutoData(null)]
        public async Task Update_Throws_Exception_When_Request_Is_Null(
            Update.Request request, 
            CancellationToken cancellationToken, 
            int userId,
            bool IsAdmin,
            int contentId,
            int categoryId)
        {
            // Arrange
            ConfigureMoqForUpdateMethod(userId.ToString(), bool IsAdmin, contentId, categoryId);

            // Act
            await Assert.ThrowsAsync<ContentUpdateRequestIsNullException>(
                async () => await _contentServiceV1.Update(request, cancellationToken));

        }
        private void ConfigureMoqForUpdateMethod(string userId, bool IsAdmin, int contentId, int categoryId)
        {
            var category = new Domain.Category();
            category.Id = categoryId;

            _identityServiceMock
                .Setup(_ => _.GetCurrentUserId(It.IsAny<CancellationToken>()))
                .ReturnsAsync(userId)
                .Verifiable();
            _identityServiceMock
                .Setup(_ => _.IsInRole(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(IsAdmin)
                .Verifiable();
            _contentRepositoryMock
                .Setup(_ => _.Save(It.IsAny<Domain.Content>(), It.IsAny<CancellationToken>()))
                .Callback((Domain.Content content, CancellationToken ct) => content.Id = contentId);
            _categoryRepositoryMock
                .Setup(_ => _.FindById(It.IsAny<int>(), It.IsAny<CancellationToken>())).Returns(async () => category)
                .Callback((int _categoryId, CancellationToken ct) => _categoryId = categoryId);
        }
    }
}
