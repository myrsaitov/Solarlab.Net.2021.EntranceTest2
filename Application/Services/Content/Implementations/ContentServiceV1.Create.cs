using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Services.Category.Contracts.Exceptions;
using SL2021.Application.Services.Content.Contracts;
using SL2021.Application.Services.Content.Interfaces;

namespace SL2021.Application.Services.Content.Implementations
{
    public sealed partial class ContentServiceV1 : IContentService
    {
        public async Task<Create.Response> Create(
            Create.Request request, 
            CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var categoryRequest = new Category.Contracts.GetById.Request()
            {
                Id = request.CategoryId
            };

            var category = await _categoryRepository.FindById(
                categoryRequest.Id,
                cancellationToken);

            if (category is null)
            {
                throw new CategoryNotFoundException(categoryRequest.Id);
            }

            string userId = await _identityService.GetCurrentUserId(cancellationToken);

            var content = _mapper.Map<Domain.Content>(request);
            content.IsDeleted = false;
            content.OwnerId = userId;
            content.CreatedAt = DateTime.UtcNow;
            content.Category = category;

            if (request.TagBodies is not null)
            {
                content.Tags = new List<Domain.Tag>();
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

            return new Create.Response
            {
                Id = content.Id
            };
        }
    }
}