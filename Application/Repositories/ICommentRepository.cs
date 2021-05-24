using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using SL2021.Domain;

namespace SL2021.Application.Repositories
{
    public interface ICommentRepository : IRepository<Comment, int>
    {
        Task<Comment> FindByIdWithUserInclude(
            int id, 
            CancellationToken cancellationToken);
        Task<Comment> FindByIdWithUserAndCommentsInclude(
            int id, 
            CancellationToken cancellationToken);
        Task<IEnumerable<Comment>> GetPagedWithOwnerAndCommentInclude(
            Expression<Func<Comment, bool>> predicate,
            int offset,
            int limit,
            CancellationToken cancellationToken);
    }
}