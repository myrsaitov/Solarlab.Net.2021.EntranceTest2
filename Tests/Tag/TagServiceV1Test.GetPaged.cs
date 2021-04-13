using WidePictBoard.Application.Services.Tag.Contracts;
using WidePictBoard.Application.Services.Tag.Contracts.Exceptions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using AutoFixture.Xunit2;
using System.Collections.Generic;
using WidePictBoard.Application.Services.PagedBase.Contracts;

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
            ConfigureMoqForGetPagedMethod(request);

            // Act
            var response = await _tagServiceV1.GetPaged(request, cancellationToken);

            // Assert
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
            await Assert.ThrowsAsync<TagGetPagedRequestIsNullException>(
                async () => await _tagServiceV1.GetPaged(request, cancellationToken));

        }
        private void ConfigureMoqForGetPagedMethod(Paged.Request request)
        {
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
                .Setup(_ => _.GetPaged(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(responce)
                .Verifiable();
        }
    }
}

