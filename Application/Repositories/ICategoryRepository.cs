using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WidePictBoard.Application.Repositories
{
    public interface ICategoryRepository : IRepository<Domain.Category, int>
    {
        Task<IEnumerable<Domain.Category>> GetAll(CancellationToken cancellationToken);
    }
}