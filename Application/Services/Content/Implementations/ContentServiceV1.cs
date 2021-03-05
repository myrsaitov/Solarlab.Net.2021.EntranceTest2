using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Application.Services.Content.Contracts;
using WidePictBoard.Application.Services.Content.Contracts.Exceptions;
using WidePictBoard.Application.Services.Content.Interfaces;
using WidePictBoard.Application.Services.User.Interfaces;
using WidePictBoard.Domain.General.Exceptions;
using WidePictBoard.Domain.Shared.Exceptions;

namespace WidePictBoard.Application.Services.Content.Implementations
{
    public sealed class ContentServiceV1 : IContentService
    {
        private readonly IRepository<Domain.Content, int> _repository;
        private readonly IUserService _userService;

        public ContentServiceV1(IUserService userService, IRepository<Domain.Content, int> repository)
        {
            _userService = userService;
            _repository = repository;
        }

        public async Task<Create.Response> Create(Create.Request request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetCurrent(cancellationToken);
            var content = new Domain.Content
            {
                Name = request.Name,
                Price = request.Price,
                Status = Domain.Content.Statuses.Created,
                OwnerId = user.Id,
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

            content.Status = Domain.Content.Statuses.Payed;
            content.UpdatedAt = DateTime.UtcNow;
            await _repository.Save(content, cancellationToken);
        }

        public async Task Delete(Delete.Request request, CancellationToken cancellationToken)
        {
            var content = await _repository.FindById(request.Id, cancellationToken);
            if (content == null)
            {
                throw new ContentNotFoundException(request.Id);
            }

            var user = await _userService.GetCurrent(cancellationToken);
            if (content.Owner.Id != user.Id)
            {
                throw new NoRightsException("Нет прав для выполнения операции.");
            }

            content.Status = Domain.Content.Statuses.Closed;
            content.UpdatedAt = DateTime.UtcNow;
            await _repository.Save(content, cancellationToken);
        }

        public async Task<GetById.Response> GetById(GetById.Request request, CancellationToken cancellationToken)
        {
            var content = await _repository.FindById(request.Id, cancellationToken);
            if (content == null)
            {
                throw new ContentNotFoundException(request.Id);
            }

            return new GetById.Response
            {
                Name = content.Name,
                Owner = new GetById.Response.OwnerResponse
                {
                    Id = content.Owner.Id,
                    Name = content.Owner.Name
                },
                Price = content.Price,
                Status = content.Status.ToString()
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

            var contents = await _repository.GetPaged(
                request.Offset, request.Limit, cancellationToken
            );


            return new GetPaged.Response
            {
                Items = contents.Select(content => new GetPaged.Response.AdResponse
                {
                    Id = content.Id,
                    Name = content.Name,
                    Price = content.Price,
                    Status = content.Status.ToString()
                }),
                Total = total,
                Offset = request.Offset,
                Limit = request.Limit
            };
        }
    }
}