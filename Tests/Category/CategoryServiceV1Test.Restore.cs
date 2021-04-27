using SL2021.Application.Services.Category.Contracts;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using AutoFixture.Xunit2;
using System;
using SL2021.Application.Services.Category.Contracts.Exceptions;
using SL2021.Domain.General.Exceptions;

namespace SL2021.Tests.Category
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
            var category = new Domain.Category();

            _categoryRepositoryMock
                .Setup(_ => _.FindById(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(category)
                .Callback((int _categoryId, CancellationToken ct) => category.Id = _categoryId)
                .Verifiable();

            _identityServiceMock
                .Setup(_ => _.GetCurrentUserId(It.IsAny<CancellationToken>()))
                .ReturnsAsync(userId.ToString())
                .Verifiable();

            _identityServiceMock
                .Setup(_ => _.IsInRole(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true)
                .Verifiable();

            _categoryRepositoryMock
                .Setup(_ => _.Save(
                    It.IsAny<Domain.Category>(),
                    It.IsAny<CancellationToken>()))
                .Callback((Domain.Category category, CancellationToken ct) => category.Id = categoryId);

            // Act
            await _categoryServiceV1.Restore(
                request, 
                cancellationToken);

            // Assert
            _identityServiceMock.Verify();
            _categoryRepositoryMock.Verify();
        }
        [Theory]
        [AutoData]
        public async Task Restore_Throws_Exception_When_No_Rights(
            Restore.Request request,
            CancellationToken cancellationToken)
        {
            // Arrange
            var category = new Domain.Category();

            _categoryRepositoryMock
                .Setup(_ => _.FindById(
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(category)
                .Callback((int _categoryId, CancellationToken ct) => category.Id = _categoryId)
                .Verifiable();

            _identityServiceMock
                .Setup(_ => _.GetCurrentUserId(It.IsAny<CancellationToken>()))
                .ReturnsAsync("")
                .Verifiable();

            _identityServiceMock
                .Setup(_ => _.IsInRole(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(false)
                .Verifiable();

            // Act
            await Assert.ThrowsAsync<NoRightsException>(
                async () => await _categoryServiceV1.Restore(
                    request,
                    cancellationToken));
        }
        [Theory]
        [AutoData]
        public async Task Restore_Throws_Exception_When_Category_Is_Null(
            Restore.Request request,
            CancellationToken cancellationToken)
        {
            // Act
            await Assert.ThrowsAsync<CategoryNotFoundException>(
                async () => await _categoryServiceV1.Restore(
                    request,
                    cancellationToken));
        }
        [Theory]
        [InlineAutoData(null)]
        public async Task Restore_Throws_Exception_When_Request_Is_Null(
            Restore.Request request, 
            CancellationToken cancellationToken)
        {
            // Act
            await Assert.ThrowsAsync<ArgumentNullException>(
                async () => await _categoryServiceV1.Restore(
                    request, 
                    cancellationToken));
        }
    }
}
