using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Domain.General;


namespace WidePictBoard.Application.Repositories
{
    public interface ICategoryRepository : IRepository<Domain.Category, int>
    {
        Task<Domain.Category> FindByIdWithUserInclude(int id, CancellationToken cancellationToken);

        Task<Domain.Category> FindByIdWithUserAndCategory(int id, CancellationToken cancellationToken);
    }
}