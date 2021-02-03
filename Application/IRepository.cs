using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace WidePictBoard.Application
{
    public interface IRepository<TEntity, TId>
    {
        public Task<TEntity> FindById(TId id, CancellationToken token);
        public Task Save(TEntity entity, CancellationToken token);
        public Task<TEntity> FindWhere(Expression<Func<TEntity, bool>> query, CancellationToken token);
    }
}