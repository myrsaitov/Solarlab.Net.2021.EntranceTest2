using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Repositories;
using SL2021.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace SL2021.Infrastructure.DataAccess.Repositories
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
        public async Task<Content> FindByIdWithUserAndTagsInclude(int id, CancellationToken cancellationToken)
        {
            return await DbСontext
                .Set<Content>()
                .Include(a => a.Owner)
                .Include(a => a.Tags)
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
        public async Task<Content> FindByIdWithUserAndImages(int id, CancellationToken cancellationToken)
        {
            return await DbСontext
                .Set<Content>()
                .Include(a => a.Owner)
                .Include(a => a.Images)
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }
        public async Task<int> CountWithOutDeleted(CancellationToken cancellationToken)
        {
            var data = DbСontext
                .Set<Content>()
                .AsNoTracking(); ;

            return await data
                .Where(c => c.IsDeleted == false)
                .CountAsync(cancellationToken);
        }
        public async Task<int> CountWithOutDeleted(
            Expression<Func<Content, bool>> predicate,
            CancellationToken cancellationToken)
        {
            var data = DbСontext
                .Set<Content>()
                .AsNoTracking(); ;

            return await data
                .Where(c => c.IsDeleted == false)
                .Where(predicate)
                .CountAsync(cancellationToken);
        }
        public async Task<IEnumerable<Content>> GetPagedWithTagsAndOwnerAndCategoryInclude(
            int offset,
            int limit,
            CancellationToken cancellationToken)
        {
            var data = DbСontext
                .Set<Content>()
                .Include(a => a.Tags)
                .Include(a => a.Owner)
                .Include(a => a.Category)
                .AsNoTracking(); ;

            return await data
                .Where(c => c.IsDeleted == false)
                .OrderBy(e => e.Id)
                .Skip(offset)
                .Take(limit)
                .ToListAsync(cancellationToken);
        }
        public async Task<IEnumerable<Content>> GetPagedWithTagsAndOwnerAndCategoryInclude(
            Expression<Func<Content, bool>> predicate,
            int offset,
            int limit,
            CancellationToken cancellationToken)
        {
            var data = DbСontext
                .Set<Content>()
                .Include(a => a.Tags)
                .Include(a => a.Owner)
                .Include(a => a.Category)
                .AsNoTracking();

            return await data
                .Where(c => c.IsDeleted == false)
                .Where(predicate)
                .OrderBy(e => e.Id)
                .Skip(offset)
                .Take(limit)
                .ToListAsync(cancellationToken);
        }
    }
}