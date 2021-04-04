using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Domain;
using Microsoft.EntityFrameworkCore;

namespace WidePictBoard.Infrastructure.DataAccess.Repositories
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
                .Include(a => a.ChildComments)
                //.AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }
    }
}