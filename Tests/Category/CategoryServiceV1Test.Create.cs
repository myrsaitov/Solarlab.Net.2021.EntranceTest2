using WidePictBoard.Application.Services.Category.Contracts;
using WidePictBoard.Application.Services.Category.Contracts.Exceptions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using AutoFixture.Xunit2;
using System.Linq.Expressions;
using System;

namespace WidePictBoard.Tests.Category
{
    public partial class CategoryServiceV1Test
    {
        [Theory]
        [AutoData]
        public async Task Create_Returns_Response_Success(
            Create.Request request, 
            CancellationToken cancellationToken, 
            int userId, 
            int categoryId)
        {
            // Arrange
            ConfigureMoqForCreateMethod(userId.ToString(), categoryId);

            // Act
            var response = await _categoryServiceV1.Create(request, cancellationToken);

            // Assert
            _identityServiceMock.Verify();
            _categoryRepositoryMock.Verify();
            Assert.NotNull(response);
            Assert.NotEqual(default, response.Id);
        }
        [Theory]
        [InlineAutoData(null)]
        public async Task Create_Throws_Exception_When_Request_Is_Null(
            Create.Request request, 
            CancellationToken cancellationToken, 
            int userId, 
            int categoryId
            )
        {
            // Arrange
            ConfigureMoqForCreateMethod(userId.ToString(), categoryId);

            // Act
            await Assert.ThrowsAsync<CategoryCreateRequestIsNullException>(
                async () => await _categoryServiceV1.Create(request, cancellationToken));

        }
        private void ConfigureMoqForCreateMethod(string userId, int categoryId)
        {
            var category = new Domain.Category();
            category.Id = categoryId;

            _identityServiceMock
                .Setup(_ => _.GetCurrentUserId(It.IsAny<CancellationToken>()))
                .ReturnsAsync(userId)
                .Verifiable();

            _categoryRepositoryMock
                .Setup(_ => _.FindById(It.IsAny<int>(), It.IsAny<CancellationToken>())).Returns(async () => category)
                .Callback((int _categoryId, CancellationToken ct) => _categoryId = categoryId);

            _categoryRepositoryMock
                .Setup(_ => _.Save(It.IsAny<Domain.Category>(), It.IsAny<CancellationToken>()))
                .Callback((Domain.Category category, CancellationToken ct) => category.Id = categoryId);
        }
    }
}
