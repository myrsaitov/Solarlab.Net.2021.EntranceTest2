using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Domain.General;


namespace WidePictBoard.Application.Repositories
{
    public interface ICommentRepository : IRepository<Domain.Comment, int>
    {
        Task<Domain.Comment> FindByIdWithUserInclude(int id, CancellationToken cancellationToken);

        Task<Domain.Comment> FindByIdWithUserAndCategory(int id, CancellationToken cancellationToken);
    }
}