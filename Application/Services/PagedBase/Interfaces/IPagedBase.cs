using MapsterMapper;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Application.Services.Content.Contracts;
using WidePictBoard.Application.Services.PagedBase.Contracts;
using WidePictBoard.Domain.General;

namespace WidePictBoard.Application.Services.PagedBase.Interfaces
{
    public interface IPagedBase<TPagedResponse, TSingleResponce, TPagedRequest, TEntity>
        where TEntity : Entity<int>
    {
        Task<Paged.Response<TSingleResponce>> GetPaged(TPagedRequest request, IRepository<TEntity, int> repository, IMapper mapper, CancellationToken cancellationToken);
    }
}