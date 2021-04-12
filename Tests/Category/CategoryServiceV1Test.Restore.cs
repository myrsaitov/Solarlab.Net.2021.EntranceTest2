using WidePictBoard.Application.Services.Category.Contracts;
using WidePictBoard.Application.Services.Category.Contracts.Exceptions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using AutoFixture.Xunit2;

namespace WidePictBoard.Tests.Category
{
    public partial class CategoryServiceV1Test
    {
        [Theory]
        [AutoData]
        public async Task Restore_Returns_Response_Success(
            Restore.Request request, 
            CancellationToken cancellationToken, 
            int userId,
            int categoryId)
        {
            // Arrange
            ConfigureMoqForRestoreMethod(userId.ToString(), categoryId);

            // Act
            await _categoryServiceV1.Restore(request, cancellationToken);

            // Assert
            _identityServiceMock.Verify();
            _categoryRepositoryMock.Verify();
        }
        [Theory]
        [InlineAutoData(null)]
        public async Task Restore_Throws_Exception_When_Request_Is_Null(
            Restore.Request request, 
            CancellationToken cancellationToken, 
            int userId, 
            int categoryId
            )
        {
            // Arrange
            ConfigureMoqForRestoreMethod(userId.ToString(), categoryId);

            // Act
            await Assert.ThrowsAsync<CategoryRestoreRequestIsNullException>(
                async () => await _categoryServiceV1.Restore(request, cancellationToken));

        }
        private void ConfigureMoqForRestoreMethod(string userId, int categoryId)
        {
            var category = new Domain.Category();

            _categoryRepositoryMock
                .Setup(_ => _.FindById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(category)
                .Callback((int _categoryId, CancellationToken ct) => _categoryId = categoryId)
                .Verifiable();

            _identityServiceMock
                .Setup(_ => _.GetCurrentUserId(It.IsAny<CancellationToken>()))
                .ReturnsAsync(userId)
                .Verifiable();

            _identityServiceMock
                .Setup(_ => _.IsInRole(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true)
                .Verifiable();

            _categoryRepositoryMock
                .Setup(_ => _.Save(It.IsAny<Domain.Category>(), It.IsAny<CancellationToken>()))
                .Callback((Domain.Category category, CancellationToken ct) => category.Id = categoryId);
        }
    }
}
