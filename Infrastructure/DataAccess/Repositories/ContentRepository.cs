using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

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

        public async Task<Content> FindByIdWithUserAndCategoryAndTags(int id, CancellationToken cancellationToken)
        {
            return await DbСontext
                .Set<Content>()
                .Include(a => a.Owner)
                .Include(a => a.Category)
                .Include(a => a.Category.ChildCategories)
                .Include(a => a.Category.ParentCategory)
                .Include(a => a.Tags)
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }
    }
}