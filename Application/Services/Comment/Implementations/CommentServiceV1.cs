using MapsterMapper;
using System;
using System.Collections.Generic;
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
using WidePictBoard.Application.Services.Content.Contracts.Exceptions;
using System.Linq;

namespace WidePictBoard.Application.Services.Comment.Implementations
{
    public sealed class CommentServiceV1 : ICommentService
    {
        private readonly IContentRepository _contentRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public CommentServiceV1(
            ICommentRepository commentRepository, 
            IContentRepository contentRepository, 
            IIdentityService identityService, 
            IMapper mapper)
        {
            _contentRepository = contentRepository;
            _commentRepository = commentRepository;
            _identityService = identityService;
            _mapper = mapper;
        }
        public async Task<Create.Response> Create(
            Create.Request request, 
            CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new CommentCreateRequestIsNullException();
            }

            var contentRequest = new Content.Contracts.GetById.Request()
            {
                Id = request.ContentId
            };

            var content = await _contentRepository.FindByIdWithUserInclude(
                contentRequest.Id,
                cancellationToken);

            if (content == null)
            {
                throw new ContentNotFoundException(contentRequest.Id);
            };

            string userId = await _identityService.GetCurrentUserId(cancellationToken);

            var comment = _mapper.Map<Domain.Comment>(request); 
            comment.IsDeleted = false;
            comment.OwnerId = userId;
            comment.CreatedAt = DateTime.UtcNow;

            var parentCommentIdNulable = request.ParentCommentId;
            if (parentCommentIdNulable != null)
            {
                int parentCommentId = (int)parentCommentIdNulable;
                
                var parentComment = await _commentRepository.FindByIdWithUserAndCommentsInclude(
                    parentCommentId, 
                    cancellationToken);
                
                if (parentComment != null)
                {
                    if (parentComment.ChildComments != null)
                    {
                        parentComment.ChildComments.Add(comment);
                    }
                    else
                    {
                        parentComment.ChildComments = new List<Domain.Comment>()
                        {
                            comment
                        };
                    }
                    await _commentRepository.Save(parentComment, cancellationToken);

                    comment.ParentComment = parentComment;
                }
                else 
                {
                    throw new ParentCommentNotFoundException(request.ParentCommentId);
                }

                await _commentRepository.Save(comment, cancellationToken);
            }

            content.Comments = new List<Domain.Comment>()
            {
                comment
            };

            await _contentRepository.Save(content, cancellationToken);

            await _commentRepository.Save(comment, cancellationToken);

            return new Create.Response
            {
                Id = comment.Id
            };
        }
        public async Task<Update.Response> Update(
            Update.Request request, 
            CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new CommentUpdateRequestIsNullException();
            }

            var comment = await _commentRepository.FindByIdWithUserInclude(
                request.Id, 
                cancellationToken);

            if (comment == null)
            {
                throw new CommentNotFoundException(request.Id);
            }

            var userId = await _identityService.GetCurrentUserId(cancellationToken);

            var isAdmin = await _identityService.IsInRole(
                userId, 
                RoleConstants.AdminRole, 
                cancellationToken);

            if (!isAdmin && comment.OwnerId != userId)
            {
                throw new NoRightsException("Нет прав для выполнения операции.");
            }

            comment.Body = request.Body;
            comment.IsDeleted = false;
            comment.UpdatedAt = DateTime.UtcNow;
            await _commentRepository.Save(comment, cancellationToken);

            return new Update.Response
            {
                Id = comment.Id
            };
        }
        public async Task Delete(
            Delete.Request request,
            CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new CommentDeleteRequestIsNullException();
            }

            var comment = await _commentRepository.FindByIdWithUserInclude(
                request.Id, 
                cancellationToken);

            if (comment == null)
            {
                throw new CommentNotFoundException(request.Id);
            }

            var userId = await _identityService.GetCurrentUserId(cancellationToken);

            var isAdmin = await _identityService.IsInRole(
                userId, 
                RoleConstants.AdminRole, 
                cancellationToken);

            if (!isAdmin && comment.OwnerId != userId)
            {
                throw new NoRightsException("Нет прав для выполнения операции.");
            }

            comment.IsDeleted = true;
            comment.UpdatedAt = DateTime.UtcNow;
            await _commentRepository.Save(comment, cancellationToken);
        }
        public async Task Restore(
            Restore.Request request, 
            CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new CommentRestoreRequestIsNullException();
            }

            var comment = await _commentRepository.FindByIdWithUserInclude(
                request.Id, 
                cancellationToken);

            if (comment == null)
            {
                throw new CommentNotFoundException(request.Id);
            }

            var userId = await _identityService.GetCurrentUserId(cancellationToken);

            var isAdmin = await _identityService.IsInRole(
                userId, 
                RoleConstants.AdminRole, 
                cancellationToken);

            if (!isAdmin && comment.OwnerId != userId)
            {
                throw new NoRightsException("Нет прав для выполнения операции.");
            }

            comment.IsDeleted = false;
            comment.UpdatedAt = DateTime.UtcNow;
            await _commentRepository.Save(comment, cancellationToken);
        }
        public async Task<GetById.Response> GetById(
            GetById.Request request, 
            CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new CommentGetByIdRequestIsNullException();
            }

            var comment = await _commentRepository.FindByIdWithUserAndCommentsInclude(
                request.Id, 
                cancellationToken);

            if (comment == null)
            {
                throw new CommentNotFoundException(request.Id);
            }

            return _mapper.Map<GetById.Response>(comment);
        }
        public async Task<Paged.Response<GetById.Response>> GetPaged(
            int contentId, 
            Paged.Request request, 
            CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new CommentGetPagedRequestIsNullException();
            }

            var content = await _contentRepository.FindById(
                contentId,
                cancellationToken);

            if (content == null)
            {
                throw new ContentNotFoundException(contentId);
            }

            var total = await _commentRepository.Count(
                a => a.ContentId == contentId, 
                cancellationToken);

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

            var entities = await _commentRepository.GetPaged(
                a => a.ContentId == contentId,
                request.Page,
                request.PageSize,
                cancellationToken
            );

            return new Paged.Response<GetById.Response>
            {
                Items = entities.Select(entity => _mapper.Map<GetById.Response>(entity)),
                Total = total,
                Offset = request.Page,
                Limit = request.PageSize
            };
        }
    }
}