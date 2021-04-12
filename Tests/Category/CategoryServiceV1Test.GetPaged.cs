using WidePictBoard.Application.Services.Category.Contracts;
using WidePictBoard.Application.Services.Category.Contracts.Exceptions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using AutoFixture.Xunit2;
using System.Collections.Generic;
using WidePictBoard.Application.Services.PagedBase.Contracts;

namespace WidePictBoard.Tests.Category
{
    public partial class CategoryServiceV1Test
    {
        [Theory]
        [AutoData]
        public async Task GetPaged_Returns_Response_Success(
            Paged.Request request, 
            CancellationToken cancellationToken, 
            int userId,
            string categoryTitle,
            string categoryBody,
            string[] tagBodies,
            int categoryId)
        {
            // Arrange
            ConfigureMoqForGetPagedMethod(
                request,
                userId.ToString(),
                categoryTitle,
                categoryBody,
                tagBodies,
                categoryId);

            // Act
            var response = await _categoryServiceV1.GetPaged(request, cancellationToken);

            // Assert
            _identityServiceMock.Verify();
            _categoryRepositoryMock.Verify();
            _categoryRepositoryMock.Verify();
            _tagRepositoryMock.Verify();
            Assert.NotNull(response);
        }
        [Theory]
        [InlineAutoData(null)]
        public async Task GetPaged_Throws_Exception_When_Request_Is_Null(
            Paged.Request request, 
            CancellationToken cancellationToken, 
            int userId,
            string categoryTitle,
            string categoryBody,
            string[] tagBodies,
            int categoryId)
        {
            // Arrange
            ConfigureMoqForGetPagedMethod(
                request,
                userId.ToString(),
                categoryTitle,
                categoryBody,
                tagBodies,
                categoryId);

            // Act
            await Assert.ThrowsAsync<CategoryGetPagedRequestIsNullException>(
                async () => await _categoryServiceV1.GetPaged(request, cancellationToken));

        }
        private void ConfigureMoqForGetPagedMethod(
            Paged.Request request,
            string userId,
            string categoryTitle,
            string categoryBody,
            string[] tagBodies,
            int categoryId)
        {
           /* int categoryCount = 3;

            var responce = new List<Domain.Category>();

            for (int categoryId = 1; categoryId <= categoryCount; categoryId++)
            {
                var category = new Domain.Category()
                {
                    Id = categoryId,

                };

                int tagId = 1;
                foreach (string body in tagBodies)
                {
                    var tag = new Domain.Tag()
                    {
                        Id = tagId++,
                        Body = body
                    };
                    category.Tags.Add(tag);
                }
                responce.Add(category);
            }
            
            _categoryRepositoryMock
                .Setup(_ => _.Count(It.IsAny<CancellationToken>()))
                .ReturnsAsync(categoryCount)
                .Verifiable();

            _categoryRepositoryMock
                .Setup(_ => _.GetPagedWithTagsInclude(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(responce)
                .Verifiable();*/
        }
    }
}
