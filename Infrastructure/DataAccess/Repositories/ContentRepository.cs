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
    public sealed class ContentRepository : EfRepository<Content, int>, IContentRepository
    {
        public ContentRepository(DatabaseContext dbСontext) : base(dbСontext)
        {
        }

        public async Task<Content> FindByIdWithUserInclude(int id, CancellationToken cancellationToken)
        {
            return await DbСontext
                .Set<Content>()
                .Include(a => a.Owner)
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }

        public async Task<Content> FindByIdWithUserAndCategory(int id, CancellationToken cancellationToken)
        {
            return await DbСontext
                .Set<Content>()
                .Include(a => a.Owner)
                .Include(a => a.Category)
                .Include(a => a.Category.ChildCategories)
                .Include(a => a.Category.ParentCategory)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }
    }
}