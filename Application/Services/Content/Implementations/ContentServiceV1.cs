using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Application.Services.Content.Contracts;
using WidePictBoard.Application.Services.Content.Contracts.Exceptions;
using WidePictBoard.Application.Services.Content.Interfaces;
using WidePictBoard.Application.Services.User.Interfaces;
using WidePictBoard.Domain;
using WidePictBoard.Domain.General;
using WidePictBoard.Domain.General.Exceptions;

namespace WidePictBoard.Application.Services.Content.Implementations
{
    public sealed class ContentServiceV1 : IContentService
    {
        private readonly IContentRepository _repository;
        private readonly IUserService _userService;

        public ContentServiceV1(IUserService userService, IContentRepository repository)
        {
            _userService = userService;
            _repository = repository;
        }

        public async Task<Create.Response> Create(Create.Request request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetCurrent(cancellationToken);
            var ad = new Domain.Content
            {
                FirstName = request.Name,
                LastName = request.Name,
                Price = request.Price,
                Status = Domain.Content.Statuses.Created,
                OwnerId = user.Id,
                CreatedAt = DateTime.UtcNow
            };
            await _repository.Save(ad, cancellationToken);

            return new Create.Response
            {
                Id = ad.Id
            };
        }

        public async Task Pay(Pay.Request request, CancellationToken cancellationToken)
        {
            var ad = await _repository.FindById(request.Id, cancellationToken);

            if (ad == null)
            {
                throw new ContentNotFoundException(request.Id);
            }

            ad.Status = Domain.Content.Statuses.Payed;
            ad.UpdatedAt = DateTime.UtcNow;
            await _repository.Save(ad, cancellationToken);
        }

        public async Task Delete(Delete.Request request, CancellationToken cancellationToken)
        {
            var ad = await _repository.FindById(request.Id, cancellationToken);
            if (ad == null)
            {
                throw new ContentNotFoundException(request.Id);
            }

            var user = await _userService.GetCurrent(cancellationToken);
            if (ad.Owner.Id != user.Id)
            {
                throw new NoRightsException("Нет прав для выполнения операции.");
            }

            ad.Status = Domain.Content.Statuses.Closed;
            ad.UpdatedAt = DateTime.UtcNow;
            await _repository.Save(ad, cancellationToken);
        }

        public async Task<GetById.Response> GetById(GetById.Request request, CancellationToken cancellationToken)
        {
            var c = CategoryType.Auto;
            var ad = await _repository.FindByIdAndCategory(request.Id, CategoryType.Auto, cancellationToken);
            if (ad == null)
            {
                throw new ContentNotFoundException(request.Id);
            }

            var result = new GetById.Response
            {
                Name = $"{ad.FirstName } {ad.LastName }",
                Owner = new GetById.Response.OwnerResponse
                {
                    Id = ad.Owner.Id,
                    Name = ad.Owner.Name
                },

                Price = ad.Price,
                Status = ad.Status.ToString(),

                Category = ad.Category
            };

            return result;
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
                    Name = $"{ad.FirstName} {ad.LastName}",
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