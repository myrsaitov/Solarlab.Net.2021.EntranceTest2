using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Common;
using WidePictBoard.Application.Identity.Interfaces;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Application.Services.Content.Contracts;
using WidePictBoard.Application.Services.Content.Contracts.Exceptions;
using WidePictBoard.Application.Services.Content.Interfaces;
using WidePictBoard.Domain.General.Exceptions;
using WidePictBoard.Application.Services.PagedBase.Contracts;
using WidePictBoard.Application.Services.PagedBase.Implementations;
using WidePictBoard.Application.Services.Tag.Interfaces;

namespace WidePictBoard.Application.Services.Content.Implementations
{
    public sealed class ContentServiceV1 : IContentService
    {
        private readonly IContentRepository _repository;
        private readonly ITagRepository _tagRepository;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;
        private PagedBase<Paged.Response<GetById.Response>, GetById.Response, Paged.Request, Domain.Content> _paged;

        public ContentServiceV1(IContentRepository repository, ITagRepository tagRepository, IIdentityService identityService, IMapper mapper)
        {
            _repository = repository;
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
            content.Tags = new List<Domain.Tag>();

            foreach (var tagstr in request.TagsStr)
            {
                var tagRequest = new Tag.Contracts.Create.Request()
                {
                    Body = tagstr
                };

                var tag = _mapper.Map<Domain.Tag>(tagRequest);
                tag.CreatedAt = DateTime.UtcNow;

                content.Tags.Add(tag);
                await _tagRepository.Save(tag, cancellationToken);
                await _repository.Save(content, cancellationToken);
            }

            return new Create.Response
            {
                Id = content.Id
            };
        }

        public async Task<Update.Response> Update(Update.Request request, CancellationToken cancellationToken)
        {
            var content = await _repository.FindByIdWithUserInclude(request.Id, cancellationToken);
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
            await _repository.Save(content, cancellationToken);

            return new Update.Response
            {
                Id = content.Id
            };
        }
        public async Task Delete(Delete.Request request, CancellationToken cancellationToken)
        {
            var content = await _repository.FindByIdWithUserInclude(request.Id, cancellationToken);
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
            await _repository.Save(content, cancellationToken);
        }
        public async Task Restore(Restore.Request request, CancellationToken cancellationToken)
        {
            var content = await _repository.FindByIdWithUserInclude(request.Id, cancellationToken);
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
            await _repository.Save(content, cancellationToken);
        }

        public async Task<GetById.Response> GetById(GetById.Request request, CancellationToken cancellationToken)
        {
            var content = await _repository.FindByIdWithUserInclude(request.Id, cancellationToken);
            if (content == null)
            {
                throw new ContentNotFoundException(request.Id);
            }

            return _mapper.Map<GetById.Response>(content);
        }

        public async Task<Paged.Response<GetById.Response>> GetPaged(Paged.Request request, CancellationToken cancellationToken)
        {
            _paged = new PagedBase<Paged.Response<GetById.Response>, GetById.Response, Paged.Request, Domain.Content>();
            return await _paged.GetPaged(request, _repository, _mapper, cancellationToken);
        }
    }
}