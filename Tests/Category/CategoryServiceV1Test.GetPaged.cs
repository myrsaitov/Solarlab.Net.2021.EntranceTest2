using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using AutoFixture.Xunit2;
using System.Collections.Generic;
using SL2021.Application.Services;
using SL2021.Application.Services.Category.Contracts;
using System.Linq;
using System;

namespace SL2021.Tests.Category
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

            // Act
            var response = await _categoryServiceV1.GetPaged(
                request, 
                cancellationToken);

            // Assert
            _categoryRepositoryMock.Verify();
            Assert.NotNull(response);
            Assert.Equal(categoryCount, response.Total);
            Assert.Equal(categoryCount, response.Items.Count());
            Assert.IsType<Paged.Response<GetById.Response>>(response);
        }
        [Theory]
        [AutoData]
        public async Task GetPaged_Returns_Response_Success_Total_eq_0(
            Paged.Request request,
            CancellationToken cancellationToken)
        {
            // Arrange
            int categoryCount = 0;

            var responce = new List<Domain.Category>();

            _categoryRepositoryMock
                .Setup(_ => _.Count(It.IsAny<CancellationToken>()))
                .ReturnsAsync(categoryCount)
                .Verifiable();

            // Act
            var response = await _categoryServiceV1.GetPaged(
                request,
                cancellationToken);

            // Assert
            _categoryRepositoryMock.Verify();
            Assert.NotNull(response);
            Assert.Equal(categoryCount, response.Total);
            Assert.Equal(categoryCount, response.Items.Count());
            Assert.IsType<Paged.Response<GetById.Response>>(response);
        }
        [Theory]
        [InlineAutoData(null)]
        public async Task GetPaged_Throws_Exception_When_Request_Is_Null(
            Paged.Request request, 
            CancellationToken cancellationToken)
        {
            // Act
            await Assert.ThrowsAsync<ArgumentNullException>(
                async () => await _categoryServiceV1.GetPaged(
                    request, 
                    cancellationToken));
        }
    }
}

