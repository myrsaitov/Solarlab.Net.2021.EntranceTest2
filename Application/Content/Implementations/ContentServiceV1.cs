using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Content.Contracts;
using WidePictBoard.Application.Content.Contracts.Exceptions;
using WidePictBoard.Application.Content.Interfaces;
using WidePictBoard.Application.User.Interfaces;


namespace WidePictBoard.Application.Content.Implementations
{
    public sealed class ContentServiceV1 : IContentService
    {
        private readonly IRepository<Domain.Content, int> _repository;
        private readonly IUserService _userService;

        public ContentServiceV1(IRepository<Domain.Content, int> repository, IUserService userService)
        {
            _repository = repository;
            _userService = userService;
        }

        public async Task<Create.Response> Create(Create.Request request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetCurrent(cancellationToken);

            if (user == null)
            {
                throw new NoUserForContentCreationException($"Попытка создания объявления [{request.Name}] без пользователя.");
            }

            var content = new Domain.Content
            {
                Name = request.Name,
                Price = request.Price,
                OwnerId = user.Id,
                Status = Domain.Content.Statuses.Created,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.Save(content, cancellationToken);
            return new Create.Response
            {
                Id = content.Id
            };
        }

        public async Task<GetById.Response> GetById(GetById.Request request, CancellationToken cancellationToken)
        {
            var content = await _repository.FindById(request.Id, cancellationToken);
            if (content == null)
            {
                throw new NoContentFoundException(request.Id);
            }

            return new GetById.Response
            {
                Name = content.Name,
                Status = content.Status.ToString(),
                Price = content.Price,
                Owner = new GetById.Response.OwnerResponse
                {
                    Id = content.Owner.Id,
                    Name = content.Owner.Name
                }
            };
        }

        public async Task Delete(Delete.Request request, CancellationToken cancellationToken)
        {
            var content = await _repository.FindById(request.Id, cancellationToken);
            if (content == null)
            {
                throw new NoContentFoundException(request.Id);
            }

            if (content.Status != Domain.Content.Statuses.Created)
            {
                throw new ContentShouldBeInCreatedStateForClosingException(content.Id);
            }

            content.Status = Domain.Content.Statuses.Closed;
            content.UpdatedAt = DateTime.UtcNow;

            await _repository.Save(content, cancellationToken);
        }

        public async Task<GetPaged.Response> GetPaged(GetPaged.Request request, CancellationToken cancellationToken)
        {
            var total = await _repository.Count(cancellationToken);
            if (total == 0)
            {
                return new GetPaged.Response
                {
                    Total = 0,
                    Offset = request.Offset,
                    Limit = request.Limit
                };
            }

            var contents = await _repository.GetPaged(request.Offset, request.Limit, cancellationToken);

            return new GetPaged.Response
            {
                Items = contents.Select(a => new GetPaged.Response.Item
                {
                    Id = a.Id,
                    Name = a.Name,
                    Price = a.Price,
                    Status = a.Status.ToString()
                }),
                Total = total,
                Offset = request.Offset,
                Limit = request.Limit
            };
        }
    }
}