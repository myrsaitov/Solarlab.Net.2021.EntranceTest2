using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Domain;
using WidePictBoard.Domain.General;


namespace WidePictBoard.Application.Repositories
{
    public interface IContentRepository : IRepository<Content, int>
    {
        Task<Content> FindByIdWithUserInclude(int id, CancellationToken cancellationToken);
        
        Task<Content> FindByIdWithUserAndCategory(int id, CancellationToken cancellationToken);
     
        Task<Content> FindByIdAndCategory(int id, CategoryType categoryType, CancellationToken cancellationToken);
    }
}