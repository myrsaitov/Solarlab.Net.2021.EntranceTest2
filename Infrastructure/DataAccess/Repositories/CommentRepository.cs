using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Domain;
using WidePictBoard.Domain.General;
using Microsoft.EntityFrameworkCore;
using WidePictBoard.Infrastructure.DataAccess;
using WidePictBoard.Infrastructure.DataAccess.Repositories;

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
    }
}