﻿using WidePictBoard.Application.Services.Content.Contracts;
using WidePictBoard.Application.Services.Content.Contracts.Exceptions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using AutoFixture.Xunit2;
using System;
using System.Linq.Expressions;

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
            string body,
            int contentId, 
            int categoryId,
            int tagId)
        {
            // Arrange
            ConfigureMoqForUpdateMethod(userId.ToString(), body, contentId, categoryId, tagId);

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
            string body,
            int contentId,
            int categoryId,
            int tagId)
        {
            // Arrange
            ConfigureMoqForUpdateMethod(userId.ToString(), body, contentId, categoryId, tagId);

            // Act
            await Assert.ThrowsAsync<ContentUpdateRequestIsNullException>(
                async () => await _contentServiceV1.Update(request, cancellationToken));

        }
        private void ConfigureMoqForUpdateMethod(string userId, string body, int contentId, int categoryId, int tagId)
        {
            var content = new Domain.Content();
            content.Id = contentId;
            content.OwnerId = userId;

            var category = new Domain.Category();
            category.Id = categoryId;

            var tag = new Domain.Tag();
            tag.Body = body;

            _contentRepositoryMock
                .Setup(_ => _.FindByIdWithUserAndCategoryAndTags(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(async () => content)
                .Callback((int _contentId, CancellationToken ct) => _contentId = contentId);
            _identityServiceMock
                .Setup(_ => _.GetCurrentUserId(It.IsAny<CancellationToken>()))
                .ReturnsAsync(userId)
                .Verifiable();
            _identityServiceMock
                .Setup(_ => _.IsInRole(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false)
                .Verifiable();
            _categoryRepositoryMock
                .Setup(_ => _.FindById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(async () => category)
                .Callback((int _categoryId, CancellationToken ct) => _categoryId = categoryId);
            _tagRepositoryMock
                .Setup(_ => _.FindWhere(It.IsAny<Expression<Func<Domain.Tag, bool>>>(), It.IsAny<CancellationToken>()))
                .Returns(async () => tag)
                .Callback((Expression<Func<Domain.Tag, bool>> _predicate, CancellationToken ct) => tag.Body = body);
            _tagRepositoryMock
                .Setup(_ => _.Save(It.IsAny<Domain.Tag>(), It.IsAny<CancellationToken>()))
                .Callback((Domain.Tag tag, CancellationToken ct) => tag.Id = tagId);
            _contentRepositoryMock
                .Setup(_ => _.Save(It.IsAny<Domain.Content>(), It.IsAny<CancellationToken>()))
                .Callback((Domain.Content content, CancellationToken ct) => content.Id = contentId);
        }
    }
}
