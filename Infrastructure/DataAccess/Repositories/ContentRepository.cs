using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

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
        public async Task<IEnumerable<Content>> GetPagedWithTagsAndOwnerInclude(
            int offset,
            int limit,
            CancellationToken cancellationToken)
        {
            var data = DbСontext
                .Set<Content>()
                .Include(a => a.Tags)
                .Include(a => a.Owner)
                .AsNoTracking(); ;

            return await data
                .OrderBy(e => e.Id)
                .Take(limit)
                .Skip(offset)
                .ToListAsync(cancellationToken);
        }
        public async Task<IEnumerable<Content>> GetPagedWithTagsAndOwnerInclude(
            string tag,
            int offset,
            int limit,
            CancellationToken cancellationToken)
        {
            var data = DbСontext
                .Set<Content>()
                .Include(a => a.Tags)
                .Include(a => a.Owner)
                .AsNoTracking();

            return await data
                .Where(a => a.Tags.Any(t => t.Body == tag))
                .OrderBy(e => e.Id)
                .Take(limit)
                .Skip(offset)
                .ToListAsync(cancellationToken);
        }
    }
}