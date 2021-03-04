using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Domain.General;

namespace WidePictBoard.Application
{
    public interface IRepository<TEntity, TId>
        where TEntity : Entity<TId>
    {
        Task<TEntity> FindById(TId id, CancellationToken cancellationToken);
        Task Save(TEntity entity, CancellationToken cancellationToken);

        Task<TEntity> FindWhere(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    }
}