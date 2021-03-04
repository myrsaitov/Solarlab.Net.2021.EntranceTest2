using System;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Content.Contracts;
using WidePictBoard.Application.Content.Contracts.Exceptions;
using WidePictBoard.Application.Content.Interface;
using WidePictBoard.Application.User.Interface;


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
                Owner = new Domain.User
                {
                    Id = user.Id
                },
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
            throw new System.NotImplementedException();
        }

        public async Task Delete(Delete.Request request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}