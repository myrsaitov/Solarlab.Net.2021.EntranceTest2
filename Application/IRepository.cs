using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace WidePictBoard.Application
{
    public interface IRepository<TEntity, in TId>
    {
        Task<TEntity> FindById(TId id, CancellationToken token);
        Task Save(TEntity entity, CancellationToken token);
        Task<TEntity> FindWhere(Expression<Func<TEntity, bool>> query, CancellationToken token);
    }
}