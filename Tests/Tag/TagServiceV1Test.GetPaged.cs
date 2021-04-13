using WidePictBoard.Application.Services.Tag.Contracts.Exceptions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using AutoFixture.Xunit2;
using System.Collections.Generic;
using WidePictBoard.Application.Services;
using WidePictBoard.Application.Services.Tag.Contracts;
using System;
using System.Linq;

namespace WidePictBoard.Tests.Tag
{
    public partial class TagServiceV1Test
    {
        [Theory]
        [AutoData]
        public async Task GetPaged_Returns_Response_Success(
            Paged.Request request, 
            CancellationToken cancellationToken)
        {
            // Arrange
            int tagCount = 3;
            var responce = new List<Domain.Tag>();
            for (int tagId = 1; tagId <= tagCount; tagId++)
            {
                var tag = new Domain.Tag()
                {
                    Id = tagId,
                };
                responce.Add(tag);
            }

            _tagRepositoryMock
                .Setup(_ => _.Count(It.IsAny<CancellationToken>()))
                .ReturnsAsync(tagCount)
                .Verifiable();

            _tagRepositoryMock
                .Setup(_ => _.GetPaged(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(responce)
                .Verifiable();

            // Act
            var response = await _tagServiceV1.GetPaged(
                request, 
                cancellationToken);

            // Assert
            _tagRepositoryMock.Verify();
            Assert.NotNull(response);
            Assert.Equal(tagCount, response.Total);
            Assert.Equal(tagCount, response.Items.Count());
            Assert.IsType<Paged.Response<GetById.Response>>(response);
        }
        [Theory]
        [AutoData]
        public async Task GetPaged_Returns_Response_Success_Tags_Count_eq_0(
            Paged.Request request,
            CancellationToken cancellationToken)
        {
            // Arrange
            var tagCount = 0;

            _tagRepositoryMock
                .Setup(_ => _.Count(It.IsAny<CancellationToken>()))
                .ReturnsAsync(tagCount)
                .Verifiable();

            // Act
            var response = await _tagServiceV1.GetPaged(
                request,
                cancellationToken);

            // Assert
            _tagRepositoryMock.Verify();
            Assert.NotNull(response);
            Assert.Equal(tagCount, response.Total);
            Assert.Equal(tagCount, response.Items.Count());
            Assert.IsType<Paged.Response<GetById.Response>>(response);
        }
        [Theory]
        [InlineAutoData(null)]
        public async Task GetPaged_Throws_Exception_When_Request_Is_Null(
            Paged.Request request, 
            CancellationToken cancellationToken)
        {
            // Arrange

            // Act
            await Assert.ThrowsAsync<TagGetPagedRequestIsNullException>(
                async () => await _tagServiceV1.GetPaged(
                    request, 
                    cancellationToken));

        }
    }
}

