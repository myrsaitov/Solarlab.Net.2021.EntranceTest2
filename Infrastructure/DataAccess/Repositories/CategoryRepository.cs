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
    public sealed class CategoryRepository : EfRepository<Category, int>, ICategoryRepository
    {
        public CategoryRepository(DatabaseContext dbСontext) : base(dbСontext)
        {
        }

        public async Task<Category> FindByIdNew(int id, CancellationToken cancellationToken)
        {
            return await DbСontext
                .Set<Category>()
                .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }

    }
}