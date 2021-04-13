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
    public class PagedBase<TGetByIdResponse, TEntity, TId> : IPagedBase<TGetByIdResponse, TEntity, TId>
        where TEntity : Entity<TId>
    {
        public PagedBase()
        { 
        }
        public async Task<Paged.Response<TGetByIdResponse>> GetPaged(
            Paged.Request request, 
            IRepository<TEntity, TId> repository, 
            IMapper mapper, 
            CancellationToken cancellationToken)
        {
            var total = await repository.Count(cancellationToken);

            if (total == 0)
            {
                return new Paged.Response<TGetByIdResponse>
                {
                    Items = Array.Empty<TGetByIdResponse>(),
                    Total = total,
                    Offset = request.Page,
                    Limit = request.PageSize
                };
            }
 
            var entities = await repository.GetPaged(
                request.Page, request.PageSize, cancellationToken
            );

            return new Paged.Response<TGetByIdResponse>
            {
                Items = entities.Select(entity => mapper.Map<TGetByIdResponse>(entity)),
                Total = total,
                Offset = request.Page,
                Limit = request.PageSize
            };
        }
        public async Task<Paged.Response<TGetByIdResponse>> GetPaged(
            Expression<Func<TEntity, bool>> predicate,
            Paged.Request request, 
            IRepository<TEntity, TId> repository, 
            IMapper mapper, 
            CancellationToken cancellationToken)
        {
            var total = await repository.Count(predicate, cancellationToken);

            if (total == 0)
            {
                return new Paged.Response<TGetByIdResponse>
                {
                    Items = Array.Empty<TGetByIdResponse>(),
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

            return new Paged.Response<TGetByIdResponse>
            {
                Items = entities.Select(entity => mapper.Map<TGetByIdResponse>(entity)),
                Total = total,
                Offset = request.Page,
                Limit = request.PageSize
            };
        }
    }
}
