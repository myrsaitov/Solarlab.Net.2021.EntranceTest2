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
using WidePictBoard.Application.Services.User.Interfaces;
using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Application.Services.Comment.Implementations
{
    public sealed class CommentServiceV1 : ICommentService
    {
        private readonly ICommentRepository _repository;
        private readonly IIdentityService _identityService;

        public CommentServiceV1(ICommentRepository repository, IIdentityService identityService)
        {
            _repository = repository;
            _identityService = identityService;
        }

        public async Task<Create.Response> Create(Create.Request request, CancellationToken cancellationToken)
        {
            string userId = await _identityService.GetCurrentUserId(cancellationToken);
            var  = new Domain.Comment
            {
                Price = request.Price,
                Status = Domain.Comment.Statuses.Created,
                OwnerId = userId,
                CreatedAt = DateTime.UtcNow
            };
            await _repository.Save(comment, cancellationToken);

            return new Create.Response
            {
                Id = comment.Id
            };
        }

        public async Task Pay(Pay.Request request, CancellationToken cancellationToken)
        {
            var comment = await _repository.FindById(request.Id, cancellationToken);

            if (comment == null)
            {
                throw new CommentNotFoundException(request.Id);
            }

            comment.Status = Domain.Comment.Statuses.Payed;
            comment.UpdatedAt = DateTime.UtcNow;
            await _repository.Save(comment, cancellationToken);
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

            comment.Status = Domain.Comment.Statuses.Closed;
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

            return new GetById.Response
            {
                Owner = new GetById.Response.OwnerResponse
                {
                    Id = comment.Owner.Id,
                    Name = $"{comment.Owner.FirstName} {comment.Owner.LastName} {comment.Owner.MiddleName}".Trim()
                },
                Price = comment.Price,
                Status = comment.Status.ToString()
            };
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
                    Items = Array.Empty<GetPaged.Response.AdResponse>(),
                    Total = total,
                    Offset = request.Offset,
                    Limit = request.Limit
                };
            }

            var ads = await _repository.GetPaged(
                request.Offset, request.Limit, cancellationToken
            );


            return new GetPaged.Response
            {
                Items = ads.Select(ad => new GetPaged.Response.AdResponse
                {
                    Id = ad.Id,
                    Name = $"TEST",
                    Price = ad.Price,
                    Status = ad.Status.ToString()
                }),
                Total = total,
                Offset = request.Offset,
                Limit = request.Limit
            };
        }
    }
}