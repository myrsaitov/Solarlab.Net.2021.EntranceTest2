using MapsterMapper;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Common;
using WidePictBoard.Application.Identity.Interfaces;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Application.Services.Comment.Contracts;
using WidePictBoard.Application.Services.Comment.Contracts.Exceptions;
using WidePictBoard.Application.Services.Comment.Interfaces;
using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Application.Services.Comment.Implementations
{
    public sealed class CommentServiceV1 : ICommentService
    {
        private readonly ICommentRepository _repository;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

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
            comment.Status = Domain.General.CommentStatus.Active;
            comment.OwnerId = userId;
            comment.CreatedAt = DateTime.UtcNow;

            await _repository.Save(comment, cancellationToken);

            return new Create.Response
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

            comment.Status = Domain.General.CommentStatus.Deleted;
            comment.UpdatedAt = DateTime.UtcNow;
            await _repository.Save(comment, cancellationToken);
        }

        public async Task<GetById.Response> GetById(GetById.Request request, CancellationToken cancellationToken)
        {
            var comment = await _repository.FindByIdWithUserInclude(request.Id, cancellationToken);
            if (comment == null)
            {
                throw new CommentNotFoundException(request.Id);
            }

            return _mapper.Map<GetById.Response>(comment);
        }

        public async Task<GetPaged.Response> GetPaged(GetPaged.Request request, CancellationToken cancellationToken)
        {
            var total = await _repository.Count(
                cancellationToken
            );

            if (total == 0)
            {
                return new GetPaged.Response
                {
                    Items = Array.Empty<GetPaged.Response.CommentResponse>(),
                    Total = total,
                    Offset = request.Page,
                    Limit = request.PageSize
                };
            }

            var comments = await _repository.GetPaged(
                request.Page, request.PageSize, cancellationToken
            );

            return new GetPaged.Response
            {
                Items = comments.Select(comment =>_mapper.Map<GetPaged.Response.CommentResponse>(comment)),
                Total = total,
                Offset = request.Page,
                Limit = request.PageSize
            };
        }
    }
}