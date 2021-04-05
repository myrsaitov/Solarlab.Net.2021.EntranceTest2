using System;
using System.Linq.Expressions;
using MapsterMapper;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Application.Services.PagedBase.Contracts;
using WidePictBoard.Domain.General;

namespace WidePictBoard.Application.Services.PagedBase.Interfaces
{
    public interface IPagedBase<TGetByIdResponce, TEntity, TId>
        where TEntity : Entity<TId>
    {
        Task<Paged.Response<TGetByIdResponce>> GetPaged(Paged.Request request, IRepository<TEntity, TId> repository, IMapper mapper, CancellationToken cancellationToken);
        Task<Paged.Response<TGetByIdResponce>> GetPaged(Expression<Func<TEntity, bool>> predicate, Paged.Request request, IRepository<TEntity, TId> repository, IMapper mapper, CancellationToken cancellationToken);
    }
}