using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Domain.General;


namespace WidePictBoard.Application.Repositories
{
    public interface ICategoryRepository : IRepository<Domain.Category, int>
    {
        Task<Domain.Category> FindByIdNew(int id, CancellationToken cancellationToken);


    }
}