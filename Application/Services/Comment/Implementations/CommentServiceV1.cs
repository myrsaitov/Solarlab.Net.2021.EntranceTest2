﻿using MapsterMapper;
using System;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Common;
using WidePictBoard.Application.Identity.Interfaces;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Application.Services.Comment.Contracts;
using WidePictBoard.Application.Services.Comment.Contracts.Exceptions;
using WidePictBoard.Application.Services.Comment.Interfaces;
using WidePictBoard.Domain.General.Exceptions;
using WidePictBoard.Application.Services.PagedBase.Contracts;
using WidePictBoard.Application.Services.PagedBase.Implementations;

namespace WidePictBoard.Application.Services.Comment.Implementations
{
    public sealed class CommentServiceV1 : ICommentService
    {
        private readonly ICommentRepository _repository;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;
        private PagedBase<GetPaged.Response, GetPaged.Response.SingleResponse, GetPaged.Request, Domain.Comment> _paged;

        public CommentServiceV1(ICommentRepository repository, IIdentityService identityService, IMapper mapper)
        {
            _repository = repository;
            _identityService = identityService;
            _mapper = mapper;
        }

        public async Task<Create.Response> Create(Create.Request request, CancellationToken cancellationToken)
        {
            string userId = await _identityService.GetCurrentUserId(cancellationToken);

            var comment = _mapper.Map<Domain.Comment>(request); 
            comment.IsDeleted = false;
            comment.OwnerId = userId;
            comment.CreatedAt = DateTime.UtcNow;

            await _repository.Save(comment, cancellationToken);

            return new Create.Response
            {
                Id = comment.Id
            };
        }
        
        public async Task<Update.Response> Update(Update.Request request, CancellationToken cancellationToken)
        {
            var comment = await _repository.FindByIdWithUserInclude(request.Id, cancellationToken);
            if (comment == null)
            {
                throw new CommentNotFoundException(request.Id);
            }

            var userId = await _identityService.GetCurrentUserId(cancellationToken);
            var isAdmin = await _identityService.IsInRole(userId, RoleConstants.AdminRole, cancellationToken);

            if (!isAdmin && comment.OwnerId != userId)
            {
                throw new NoRightsException("Нет прав для выполнения операции.");
            }

            comment.Body = request.Body;
            comment.IsDeleted = false;
            comment.UpdatedAt = DateTime.UtcNow;
            await _repository.Save(comment, cancellationToken);

            return new Update.Response
            {
                Id = comment.Id
            };
        }

        public async Task Delete(Delete.Request request, CancellationToken cancellationToken)
        {
            var comment = await _repository.FindByIdWithUserInclude(request.Id, cancellationToken);
            if (comment == null)
            {
                throw new CommentNotFoundException(request.Id);
            }

            var userId = await _identityService.GetCurrentUserId(cancellationToken);
            var isAdmin = await _identityService.IsInRole(userId, RoleConstants.AdminRole, cancellationToken);

            if (!isAdmin && comment.OwnerId != userId)
            {
                throw new NoRightsException("Нет прав для выполнения операции.");
            }

            comment.IsDeleted = true;
            comment.UpdatedAt = DateTime.UtcNow;
            await _repository.Save(comment, cancellationToken);
        }

        public async Task Restore(Restore.Request request, CancellationToken cancellationToken)
        {
            var comment = await _repository.FindByIdWithUserInclude(request.Id, cancellationToken);
            if (comment == null)
            {
                throw new CommentNotFoundException(request.Id);
            }

            var userId = await _identityService.GetCurrentUserId(cancellationToken);
            var isAdmin = await _identityService.IsInRole(userId, RoleConstants.AdminRole, cancellationToken);

            if (!isAdmin && comment.OwnerId != userId)
            {
                throw new NoRightsException("Нет прав для выполнения операции.");
            }

            comment.IsDeleted = false;
            comment.UpdatedAt = DateTime.UtcNow;
            await _repository.Save(comment, cancellationToken);
        }

        public async Task<GetPaged.Response> GetPaged(GetPaged.Request request, CancellationToken cancellationToken)
        {
            _paged = new PagedBase<GetPaged.Response, GetPaged.Response.SingleResponse, GetPaged.Request, Domain.Comment>();
            return (GetPaged.Response)await _paged.GetPaged(request, _repository, _mapper, cancellationToken);
        }
    }
}