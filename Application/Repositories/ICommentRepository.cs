using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using SL2021.Domain;

namespace SL2021.Application.Repositories
{
    public interface ICommentRepository : IRepository<Domain.Comment, int>
    {
        Task<Domain.Comment> FindByIdWithUserInclude(
            int id, 
            CancellationToken cancellationToken);
        Task<Domain.Comment> FindByIdWithUserAndCommentsInclude(
            int id, 
            CancellationToken cancellationToken);
        Task<IEnumerable<Comment>> GetPagedWithOwnerInclude(
            Expression<Func<Comment, bool>> predicate,
            int offset,
            int limit,
            CancellationToken cancellationToken);
    }
}