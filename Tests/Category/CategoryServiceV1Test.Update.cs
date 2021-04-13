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
        public async Task Update_Returns_Response_Success(
            Update.Request request,
            CancellationToken cancellationToken,
            int userId,
            int categoryId)
        {
            // Arrange
            ConfigureMoqForUpdateMethod(
                request, 
                userId.ToString(), 
                categoryId);

            // Act
            var response = await _categoryServiceV1.Update(
                request, 
                cancellationToken);

            // Assert
            _identityServiceMock.Verify();
            _categoryRepositoryMock.Verify();
            Assert.NotNull(response);
            Assert.NotEqual(default, response.Id);
        }
        [Theory]
        [InlineAutoData(null)]
        public async Task Update_Throws_Exception_When_Request_Is_Null(
            Update.Request request,
            CancellationToken cancellationToken,
            int userId,
            int categoryId
            )
        {
            // Arrange
            ConfigureMoqForUpdateMethod(
                request, 
                userId.ToString(), 
                categoryId);

            // Act
            await Assert.ThrowsAsync<CategoryUpdateRequestIsNullException>(
                async () => await _categoryServiceV1.Update(
                    request, 
                    cancellationToken));

        }
        private void ConfigureMoqForUpdateMethod(
            Update.Request request, 
            string userId, 
            int categoryId)
        {
            var category = new Domain.Category()
            {
                Id = categoryId
            };

            _categoryRepositoryMock
                .Setup(_ => _.FindById(
                    It.IsAny<int>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(category)
                .Callback((int _categoryId, CancellationToken ct) => category.Id = _categoryId)
                .Verifiable();

            _identityServiceMock
                .Setup(_ => _.GetCurrentUserId(
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(userId)
                .Verifiable();

            _categoryRepositoryMock
                .Setup(_ => _.FindById(
                    It.IsAny<int>(), 
                    It.IsAny<CancellationToken>()))
                .Returns(async () => category)
                .Callback((int _categoryId, CancellationToken ct) => category.Id = _categoryId)
                .Verifiable();

            _identityServiceMock
                .Setup(_ => _.IsInRole(
                    It.IsAny<string>(), 
                    It.IsAny<string>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true)
                .Verifiable();

            _categoryRepositoryMock
                .Setup(_ => _.FindById(
                    It.IsAny<int>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(category)
                .Callback(() => category.Id = categoryId)
                .Verifiable();

            _categoryRepositoryMock
                .Setup(_ => _.Save(
                    It.IsAny<Domain.Category>(), 
                    It.IsAny<CancellationToken>()))
                .Callback((Domain.Category category, CancellationToken ct) => category.Id = categoryId);
        }
    }
}
