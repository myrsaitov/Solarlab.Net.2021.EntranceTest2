using MapsterMapper;
using System;
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
    public class PagedBase<TPagedResponse, TSingleResponce, TPagedRequest, TEntity> : IPagedBase<TPagedResponse, TSingleResponce, TPagedRequest, TEntity>
        where TPagedResponse : Paged.Response<TSingleResponce>
        where TPagedRequest : Paged.Request
        where TEntity : Entity<int>
    {
        public PagedBase()
        { 
        }

        public async Task<Paged.Response<TSingleResponce>> GetPaged(TPagedRequest request, IRepository<TEntity, int> repository, IMapper mapper, CancellationToken cancellationToken)
        {
            var total = await repository.Count(cancellationToken);

            if (total == 0)
            {
                return new Paged.Response<TSingleResponce>
                {
                    Items = Array.Empty<TSingleResponce>(),
                    Total = total,
                    Offset = request.Page,
                    Limit = request.PageSize
                };
            }
 
            var entities = await repository.GetPaged(
                request.Page, request.PageSize, cancellationToken
            );

            return new Paged.Response<TSingleResponce>
            {
                Items = entities.Select(entity => mapper.Map<TSingleResponce>(entity)),
                Total = total,
                Offset = request.Page,
                Limit = request.PageSize
            };
        }

        public async Task<Paged.Response<TSingleResponce>> GetPaged(Expression<Func<TEntity, bool>> predicate, TPagedRequest request, IRepository<TEntity, int> repository, IMapper mapper, CancellationToken cancellationToken)
        {
            var total = await repository.Count(predicate,cancellationToken);

            if (total == 0)
            {
                return new Paged.Response<TSingleResponce>
                {
                    Items = Array.Empty<TSingleResponce>(),
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

            return new Paged.Response<TSingleResponce>
            {
                Items = entities.Select(entity => mapper.Map<TSingleResponce>(entity)),
                Total = total,
                Offset = request.Page,
                Limit = request.PageSize
            };
        }
    }
}
