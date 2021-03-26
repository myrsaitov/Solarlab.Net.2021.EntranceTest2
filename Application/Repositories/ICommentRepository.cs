using System.Threading;
using System.Threading.Tasks;


namespace WidePictBoard.Application.Repositories
{
    public interface ICommentRepository : IRepository<Domain.Comment, int>
    {
        Task<Domain.Comment> FindByIdWithUserInclude(int id, CancellationToken cancellationToken);
    }
}