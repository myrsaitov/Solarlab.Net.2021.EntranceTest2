using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Services.Content.Contracts;

namespace WidePictBoard.Application.Services.PagedBase.Interfaces
{
    public interface IPagedBase<TEntity>
    {
        Task<TEntity> GetPaged(TEntity request, CancellationToken cancellationToken);
    }
}