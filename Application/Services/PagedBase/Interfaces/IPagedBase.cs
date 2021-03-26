using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Application.Services.Content.Contracts;
using WidePictBoard.Application.Services.PagedBase.Contracts;
using WidePictBoard.Domain.General;

namespace WidePictBoard.Application.Services.PagedBase.Interfaces
{
    public interface IPagedBase<TEntity>
    where TEntity : EntityMutable<int>
    {
        Task<Paged.Response<TEntity>> GetAll(Paged.Request request, CancellationToken cancellationToken);
    }
}