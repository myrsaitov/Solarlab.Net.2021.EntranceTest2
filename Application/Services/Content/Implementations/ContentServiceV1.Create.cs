using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Services.Category.Contracts.Exceptions;
using WidePictBoard.Application.Services.Content.Contracts;
using WidePictBoard.Application.Services.Content.Contracts.Exceptions;
using WidePictBoard.Application.Services.Content.Interfaces;

namespace WidePictBoard.Application.Services.Content.Implementations
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

            if (category == null)
            {
                throw new CategoryNotFoundException(categoryRequest.Id);
            }

            string userId = await _identityService.GetCurrentUserId(cancellationToken);

            var content = _mapper.Map<Domain.Content>(request);
            content.IsDeleted = false;
            content.OwnerId = userId;
            content.CreatedAt = DateTime.UtcNow;
            content.Category = category;

            content.Tags = new List<Domain.Tag>();
            foreach (string body in request.TagBodies)
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
                    await _tagRepository.Save(tag, cancellationToken);
                }
                
                content.Tags.Add(tag);
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