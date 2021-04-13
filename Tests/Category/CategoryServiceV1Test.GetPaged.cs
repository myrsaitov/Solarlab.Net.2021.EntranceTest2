using WidePictBoard.Application.Services.Category.Contracts.Exceptions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using AutoFixture.Xunit2;
using System.Collections.Generic;
using WidePictBoard.Application.Services;

namespace WidePictBoard.Tests.Category
{
    public partial class CategoryServiceV1Test
    {
        [Theory]
        [AutoData]
        public async Task GetPaged_Returns_Response_Success(
            Paged.Request request, 
            CancellationToken cancellationToken)
        {
            // Arrange
            ConfigureMoqForGetPagedMethod(request);

            // Act
            var response = await _categoryServiceV1.GetPaged(
                request, 
                cancellationToken);

            // Assert
            _identityServiceMock.Verify();
            _categoryRepositoryMock.Verify();
            _tagRepositoryMock.Verify();
            Assert.NotNull(response);
        }
        [Theory]
        [InlineAutoData(null)]
        public async Task GetPaged_Throws_Exception_When_Request_Is_Null(
            Paged.Request request, 
            CancellationToken cancellationToken)
        {
            // Arrange
            ConfigureMoqForGetPagedMethod(request);

            // Act
            await Assert.ThrowsAsync<CategoryGetPagedRequestIsNullException>(
                async () => await _categoryServiceV1.GetPaged(
                    request, 
                    cancellationToken));

        }
        private void ConfigureMoqForGetPagedMethod(Paged.Request request)
        {
            int categoryCount = 3;

            var responce = new List<Domain.Category>();

            for (int categoryId = 1; categoryId <= categoryCount; categoryId++)
            {
                var category = new Domain.Category()
                {
                    Id = categoryId,
                };

                responce.Add(category);
            }

            _categoryRepositoryMock
                .Setup(_ => _.Count(It.IsAny<CancellationToken>()))
                .ReturnsAsync(categoryCount)
                .Verifiable();

            _categoryRepositoryMock
                .Setup(_ => _.GetPaged(
                    It.IsAny<int>(), 
                    It.IsAny<int>(), 
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(responce)
                .Verifiable();
        }
    }
}

