using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Repositories;
using SL2021.Domain;
using Microsoft.EntityFrameworkCore;

namespace SL2021.Infrastructure.DataAccess.Repositories
{
    public sealed class CategoryRepository : EfRepository<Category, int>, ICategoryRepository
    {
        public CategoryRepository(DatabaseContext dbСontext) : base(dbСontext)
        {
        }
        public async Task<Category> FindByIdWithParentAndChilds(int id, CancellationToken cancellationToken)
        {
            return await DbСontext
                .Set<Category>()
                .Include(a => a.ChildCategories)
                .Include(a => a.ParentCategory)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }
    }
}