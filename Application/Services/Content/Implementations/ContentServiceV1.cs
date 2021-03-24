using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Common;
using WidePictBoard.Application.Identity.Interfaces;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Application.Services.Content.Contracts;
using WidePictBoard.Application.Services.Content.Contracts.Exceptions;
using WidePictBoard.Application.Services.Content.Interfaces;
using WidePictBoard.Application.Services.User.Interfaces;
using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Application.Services.Content.Implementations
{
    public sealed class ContentServiceV1 : IContentService
    {
        private readonly IContentRepository _repository;
        private readonly IIdentityService _identityService;

        public ContentServiceV1(IContentRepository repository, IIdentityService identityService)
        {
            _repository = repository;
            _identityService = identityService;
        }

        public async Task<Create.Response> Create(Create.Request request, CancellationToken cancellationToken)
        {
            string userId = await _identityService.GetCurrentUserId(cancellationToken);
            var content = new Domain.Content
            {
                Title = request.Title,
                Body = request.Body,
                Price = request.Price,
                CategoryId = request.CategoryId,
                Status = Domain.General.ContentStatus.Created,
                OwnerId = userId,
                CreatedAt = DateTime.UtcNow
            };
            await _repository.Save(content, cancellationToken);

            return new Create.Response
            {
                Id = content.Id
            };
        }

        public async Task Pay(Pay.Request request, CancellationToken cancellationToken)
        {
            var content = await _repository.FindById(request.Id, cancellationToken);

            if (content == null)
            {
                throw new ContentNotFoundException(request.Id);
            }

            content.Status = Domain.General.ContentStatus.Payed;
            content.UpdatedAt = DateTime.UtcNow;
            await _repository.Save(content, cancellationToken);
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

            content.Status = Domain.General.ContentStatus.Closed;
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

            return new GetById.Response
            {
                Owner = new GetById.Response.OwnerResponse
                {
                    Id = content.Owner.Id,
                    Name = $"{content.Owner.FirstName} {content.Owner.LastName} {content.Owner.MiddleName}".Trim()
                },

                Id = content.Id,

                Title = content.Title,
                Body = content.Body,
                Price = content.Price,
                Status = content.Status.ToString(),
                CategoyId = content.CategoryId
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
                    Items = Array.Empty<GetPaged.Response.ContentResponse>(),
                    Total = total,
                    Offset = request.Page,
                    Limit = request.PageSize
                };
            }

            var contents = await _repository.GetPaged(
                request.Page, request.PageSize, cancellationToken
            );


            return new GetPaged.Response
            {
                Items = contents.Select(content => new GetPaged.Response.ContentResponse
                {
                    Id = content.Id,
                    
                    Title = content.Title,
                    Body = content.Body,
                    Price = content.Price,
                    Status = content.Status.ToString(),
                    CategoyId = content.CategoryId
                }),
                Total = total,
                Offset = request.Page,
                Limit = request.PageSize
            };
        }
    }
}