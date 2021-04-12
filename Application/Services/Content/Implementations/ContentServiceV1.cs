using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Common;
using WidePictBoard.Application.Identity.Interfaces;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Application.Services.Category.Contracts.Exceptions;
using WidePictBoard.Application.Services.Content.Contracts;
using WidePictBoard.Application.Services.Content.Contracts.Exceptions;
using WidePictBoard.Application.Services.Content.Interfaces;
using WidePictBoard.Domain.General.Exceptions;
using WidePictBoard.Application.Services.PagedBase.Contracts;
using WidePictBoard.Application.Services.PagedBase.Implementations;
using System.Linq;
using Mapster;

namespace WidePictBoard.Application.Services.Content.Implementations
{
    public sealed class ContentServiceV1 : IContentService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IContentRepository _contentRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;
        private PagedBase<GetById.Response, Domain.Content, int> _paged;

        public ContentServiceV1(IContentRepository contentRepository, 
            ICategoryRepository categoryRepository, 
            ITagRepository tagRepository, 
            IIdentityService identityService, 
            IMapper mapper)
        {
            _contentRepository = contentRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            _identityService = identityService;
            _mapper = mapper;
        }
        public async Task<Create.Response> Create(Create.Request request, CancellationToken cancellationToken)
        {
            string userId = await _identityService.GetCurrentUserId(cancellationToken);

            if(request is null)
            {
                throw new ContentCreateRequestIsNullException();
            }

            var content = _mapper.Map<Domain.Content>(request);
            content.IsDeleted = false;
            content.OwnerId = userId;
            content.CreatedAt = DateTime.UtcNow;
           
            var categoryRequest = new Category.Contracts.GetById.Request()
            {
                Id = content.CategoryId
            };
            var category = await _categoryRepository.FindById(categoryRequest.Id, cancellationToken);
            if (category == null)
            {
                throw new CategoryNotFoundException(categoryRequest.Id);
            }
            content.Category = category;

            content.Tags = new List<Domain.Tag>();
            foreach (string body in request.TagBodies)
            {
                var tag = await _tagRepository.FindWhere(a => a.Body == body, cancellationToken);
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

            await _contentRepository.Save(content, cancellationToken);
            return new Create.Response
            {
                Id = content.Id
            };
        }
        public async Task<Update.Response> Update(Update.Request request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ContentUpdateRequestIsNullException();
            }

            var content = await _contentRepository.FindByIdWithUserAndCategoryAndTags(request.Id, cancellationToken);
            if (content == null)
            {
                throw new ContentNotFoundException(request.Id);
            }

            var userId = await _identityService.GetCurrentUserId(cancellationToken);
            var isAdmin = await _identityService.IsInRole(userId, RoleConstants.AdminRole, cancellationToken);

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
            var category = await _categoryRepository.FindById(categoryRequest.Id, cancellationToken);
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
            
            foreach (string body in request.TagBodies)
            {
                var tag = await _tagRepository.FindWhere(a => a.Body == body, cancellationToken);
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

            await _contentRepository.Save(content, cancellationToken);

            return new Update.Response
            {
                Id = content.Id
            };
        }
        public async Task Delete(Delete.Request request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ContentDeleteRequestIsNullException();
            }

            var content = await _contentRepository.FindByIdWithUserInclude(request.Id, cancellationToken);
            if (content == null)
            {
                throw new ContentNotFoundException(request.Id);
            }

            var userId = await _identityService.GetCurrentUserId(cancellationToken);
            var isAdmin = await _identityService.IsInRole(userId, RoleConstants.AdminRole, cancellationToken);

            if (!isAdmin && content.OwnerId != userId)
            {
                throw new NoRightsException("Нет прав для выполнения операции.");
            }

            content.IsDeleted = true;
            content.UpdatedAt = DateTime.UtcNow;
            await _contentRepository.Save(content, cancellationToken);
        }
        public async Task Restore(Restore.Request request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ContentRestoreRequestIsNullException();
            }

            var content = await _contentRepository.FindByIdWithUserInclude(request.Id, cancellationToken);
            if (content == null)
            {
                throw new ContentNotFoundException(request.Id);
            }

            var userId = await _identityService.GetCurrentUserId(cancellationToken);
            var isAdmin = await _identityService.IsInRole(userId, RoleConstants.AdminRole, cancellationToken);

            if (!isAdmin && content.OwnerId != userId)
            {
                throw new NoRightsException("Нет прав для выполнения операции.");
            }

            content.IsDeleted = false;
            content.UpdatedAt = DateTime.UtcNow;
            await _contentRepository.Save(content, cancellationToken);
        }
        public async Task<GetById.Response> GetById(GetById.Request request, CancellationToken cancellationToken)
        {
            var content = await _contentRepository.FindByIdWithUserAndCategoryAndTags(request.Id, cancellationToken);
            if (content == null)
            {
                throw new ContentNotFoundException(request.Id);
            }

            var response = _mapper.Map<GetById.Response>(content);
            response.Tags = content.Tags.Select(x => x.Body).ToArray();
            return response;
        }
        public async Task<Paged.Response<GetById.Response>> GetPaged(
            Paged.Request request,
            CancellationToken cancellationToken)
        {
            var total = await _contentRepository.Count(cancellationToken);

            if (total == 0)
            {
                return new Paged.Response<GetById.Response>
                {
                    Items = Array.Empty<GetById.Response>(),
                    Total = total,
                    Offset = request.Page,
                    Limit = request.PageSize
                };
            }

            var entities = await _contentRepository.GetPagedWithTagsInclude(
                request.Page, request.PageSize, cancellationToken
            );

            return new Paged.Response<GetById.Response>
            {
                Items = entities.Select(entity => _mapper.Map<GetById.Response>(entity)),
                Total = total,
                Offset = request.Page,
                Limit = request.PageSize
            };
        }
        public async Task<Paged.Response<GetById.Response>> GetPaged(
            string tag,
            Paged.Request request,
            CancellationToken cancellationToken)
        {
            var total = await _contentRepository.Count(a => a.Tags.Any(t => t.Body == tag), cancellationToken);

            if (total == 0)
            {
                return new Paged.Response<GetById.Response>
                {
                    Items = Array.Empty<GetById.Response>(),
                    Total = total,
                    Offset = request.Page,
                    Limit = request.PageSize
                };
            }

            var entities = await _contentRepository.GetPagedWithTagsInclude(
                tag,
                request.Page,
                request.PageSize,
                cancellationToken
            );

            var ret = new Paged.Response<GetById.Response>
            {
                Items = entities.Select(entity => entity.Adapt<GetById.Response>()),
                Total = total,
                Offset = request.Page,
                Limit = request.PageSize
            };

            return ret;
        }
    }
}