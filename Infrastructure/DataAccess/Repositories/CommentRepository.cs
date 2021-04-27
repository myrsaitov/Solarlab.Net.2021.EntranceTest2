using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Repositories;
using SL2021.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Linq.Expressions;

namespace SL2021.Infrastructure.DataAccess.Repositories
{
    public sealed class CommentRepository : EfRepository<Comment, int>, ICommentRepository
    {
        public CommentRepository(DatabaseContext dbСontext) : base(dbСontext)
        {
        }

        public async Task<Comment> FindByIdWithUserInclude(int id, CancellationToken cancellationToken)
        {
            return await DbСontext
                .Set<Comment>()
                .Include(a => a.Owner)
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }
        public async Task<Comment> FindByIdWithUserAndCommentsInclude(int id, CancellationToken cancellationToken)
        {
            return await DbСontext
                .Set<Comment>()
                .Include(a => a.Owner)
                .Include(a => a.ParentComment)
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Comment>> GetPagedWithOwnerInclude(
            Expression<Func<Comment, bool>> predicate,
            int offset,
            int limit,
            CancellationToken cancellationToken)
        {
            var data = DbСontext
                .Set<Comment>()
                .Include(a => a.Owner)
                .AsNoTracking(); ;

            return await data
                .Where(predicate)
                .OrderBy(e => e.Id)
                .Skip(offset)
                .Take(limit)
                .ToListAsync(cancellationToken);
        }
    }
}