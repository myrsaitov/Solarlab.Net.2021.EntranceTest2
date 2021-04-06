using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Application.Services.PagedBase.Contracts;
using WidePictBoard.Application.Services.PagedBase.Interfaces;
using WidePictBoard.Domain.General;

namespace WidePictBoard.Application.Services.PagedBase.Implementations
{
    public class PagedBase<TGetByIdResponce, TEntity, TId> : IPagedBase<TGetByIdResponce, TEntity, TId>
        where TEntity : Entity<TId>
    {
        public PagedBase()
        { 
        }
        public async Task<Paged.Response<TGetByIdResponce>> GetPaged(
            Paged.Request request, 
            IRepository<TEntity, TId> repository, 
            IMapper mapper, 
            CancellationToken cancellationToken)
        {
            var total = await repository.Count(cancellationToken);

            if (total == 0)
            {
                return new Paged.Response<TGetByIdResponce>
                {
                    Items = Array.Empty<TGetByIdResponce>(),
                    Total = total,
                    Offset = request.Page,
                    Limit = request.PageSize
                };
            }
 
            var entities = await repository.GetPaged(
                request.Page, request.PageSize, cancellationToken
            );

            return new Paged.Response<TGetByIdResponce>
            {
                Items = entities.Select(entity => mapper.Map<TGetByIdResponce>(entity)),
                Total = total,
                Offset = request.Page,
                Limit = request.PageSize
            };
        }
        public async Task<Paged.Response<TGetByIdResponce>> GetPaged(
            Expression<Func<TEntity, bool>> predicate,
            Paged.Request request, 
            IRepository<TEntity, TId> repository, 
            IMapper mapper, 
            CancellationToken cancellationToken)
        {
            var total = await repository.Count(predicate, cancellationToken);

            if (total == 0)
            {
                return new Paged.Response<TGetByIdResponce>
                {
                    Items = Array.Empty<TGetByIdResponce>(),
                    Total = total,
                    Offset = request.Page,
                    Limit = request.PageSize
                };
            }

            var entities = await repository.GetPaged(
                predicate,
                request.Page,
                request.PageSize,
                cancellationToken
            );

            return new Paged.Response<TGetByIdResponce>
            {
                Items = entities.Select(entity => mapper.Map<TGetByIdResponce>(entity)),
                Total = total,
                Offset = request.Page,
                Limit = request.PageSize
            };
        }
    }
}
