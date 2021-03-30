using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Domain;
using Microsoft.EntityFrameworkCore;

namespace WidePictBoard.Infrastructure.DataAccess.Repositories
{
    public sealed class CategoryRepository : EfRepository<Category, int>, ICategoryRepository
    {
        public CategoryRepository(DatabaseContext dbСontext) : base(dbСontext)
        {
        }

        public async Task<IEnumerable<Category>> GetAll(CancellationToken cancellationToken)
        {
            var data = DbСontext.Set<Category>().AsNoTracking(); ;
            return await data.OrderBy(e => e.Id).ToListAsync(cancellationToken);
        }
    }
}