using System;
using System.Linq.Expressions;
using MapsterMapper;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Application.Services.PagedBase.Contracts;
using WidePictBoard.Domain.General;
using System.Collections.Generic;
using System.Collections;

namespace WidePictBoard.Application.Services.PagedBase.Interfaces
{
    public interface IPagedBase<TGetByIdResponse, TEntity, TId>
        where TEntity : Entity<TId>
    {
        Task<Paged.Response<TGetByIdResponse>> GetPaged(
            Paged.Request request, 
            IRepository<TEntity, TId> repository, 
            IMapper mapper, 
            CancellationToken cancellationToken);
        Task<Paged.Response<TGetByIdResponse>> GetPaged(
            Expression<Func<TEntity, bool>> predicate,
            Paged.Request request, 
            IRepository<TEntity, TId> repository, 
            IMapper mapper, 
            CancellationToken cancellationToken);
    }
}