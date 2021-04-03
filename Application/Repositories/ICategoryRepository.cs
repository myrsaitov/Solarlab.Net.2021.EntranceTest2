using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Domain;

namespace WidePictBoard.Application.Repositories
{
    public interface ICategoryRepository : IRepository<Domain.Category, int>
    {
        public Task<Category> FindByIdWithParentAndChilds(int id, CancellationToken cancellationToken);
    }
}