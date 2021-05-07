using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Common;
using SL2021.Application.Services.Category.Contracts.Exceptions;
using SL2021.Application.Services.Content.Contracts;
using SL2021.Application.Services.Content.Contracts.Exceptions;
using SL2021.Application.Services.Content.Interfaces;
using SL2021.Domain.General.Exceptions;

namespace SL2021.Application.Services.Content.Implementations
{
    public sealed partial class ContentServiceV1 : IContentService
    {
        public async Task<Update.Response> Update(
            Update.Request request,
            CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var content = await _contentRepository.FindByIdWithUserAndCategoryAndTags(
                request.Id,
                cancellationToken);

            if (content == null)
            {
                throw new ContentNotFoundException(request.Id);
            }

            var userId = await _identityService.GetCurrentUserId(cancellationToken);

            var isAdmin = await _identityService.IsInRole(
                userId,
                RoleConstants.AdminRole,
                cancellationToken);

            if (!isAdmin && content.OwnerId != userId)
            {
                throw new NoRightsException("Нет прав для выполнения операции.");
            }

            content.Title = request.Title;
            content.Body = request.Body;
            content.Price = request.Price;
            content.CategoryId = request.CategoryId;

            content.IsDeleted = false;
            content.UpdatedAt = DateTime.UtcNow;

            var categoryRequest = new Category.Contracts.GetById.Request()
            {
                Id = content.CategoryId
            };
            var category = await _categoryRepository.FindById(
                categoryRequest.Id,
                cancellationToken);

            if (category == null)
            {
                throw new CategoryNotFoundException(categoryRequest.Id);
            }

            content.Category = category;
            if (content.Tags == null)
            {
                content.Tags = new List<Domain.Tag>();
            }
            content.Tags.Clear();

            if (request.TagBodies is not null)
            {
                foreach (string body in request.TagBodies)
                {
                    if (body.Length > 0)
                    {
                        var tag = await _tagRepository.FindWhere(
                        a => a.Body == body,
                        cancellationToken);

                        if (tag == null)
                        {
                            var tagRequest = new Tag.Contracts.Create.Request()
                            {
                                Body = body
                            };

                            tag = _mapper.Map<Domain.Tag>(tagRequest);
                            tag.CreatedAt = DateTime.UtcNow;
                            tag.Count = 1;

                            await _tagRepository.Save(
                                tag,
                                cancellationToken);
                        }
                        else
                        {
                            // TODO Переделать с поиском в базе, учесть удаленные объявления
                            tag.Count += 1;
                            await _tagRepository.Save(tag, cancellationToken);
                        }

                        content.Tags.Add(tag);
                    }
                }
            }

            await _contentRepository.Save(
                content, 
                cancellationToken);

            return new Update.Response
            {
                Id = content.Id
            };
        }
    }
}