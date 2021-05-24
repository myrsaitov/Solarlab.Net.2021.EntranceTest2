using System.Threading;
using System.Threading.Tasks;
using SL2021.Domain;

namespace SL2021.Application.Repositories
{
    public interface ICategoryRepository : IRepository<Category, int>
    {
        public Task<Category> FindByIdWithParentAndChilds(
            int id, 
            CancellationToken cancellationToken);
    }
}