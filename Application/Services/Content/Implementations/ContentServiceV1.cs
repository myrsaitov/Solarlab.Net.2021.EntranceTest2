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

namespace WidePictBoard.Application.Services.Content.Implementations
{
    public sealed class ContentServiceV1 : IContentService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IContentRepository _contentRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;
        private PagedBase<GetById.Response, Domain.Content> _paged;

        public ContentServiceV1(IContentRepository contentRepository, ICategoryRepository categoryRepository, ITagRepository tagRepository, IIdentityService identityService, IMapper mapper)
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
                await _contentRepository.Save(content, cancellationToken);
            }

            return new Create.Response
            {
                Id = content.Id
            };
        }

        public async Task<Update.Response> Update(Update.Request request, CancellationToken cancellationToken)
        {
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

            content.Title = request.Title;
            content.Body = request.Body;
            content.Price = request.Price;
            content.CategoryId = request.CategoryId;

            content.IsDeleted = false;
            content.UpdatedAt = DateTime.UtcNow;
            await _contentRepository.Save(content, cancellationToken);

            return new Update.Response
            {
                Id = content.Id
            };
        }
        public async Task Delete(Delete.Request request, CancellationToken cancellationToken)
        {
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
            var content = await _contentRepository.FindByIdWithUserAndCategory(request.Id, cancellationToken);
            if (content == null)
            {
                throw new ContentNotFoundException(request.Id);
            }

            return _mapper.Map<GetById.Response>(content);
        }

        public async Task<Paged.Response<GetById.Response>> GetPaged(Paged.Request request, CancellationToken cancellationToken)
        {
            _paged = new PagedBase<GetById.Response, Domain.Content>();
            return await _paged.GetPaged(request, _contentRepository, _mapper, cancellationToken);
        }
    }
}