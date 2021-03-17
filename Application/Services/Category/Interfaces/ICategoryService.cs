using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Services.Category.Contracts;

namespace WidePictBoard.Application.Services.Category.Interfaces
{
    public interface ICategoryService
    {
        Task<Create.Response> Create(Create.Request request, CancellationToken cancellationToken);

        Task Delete(Delete.Request request, CancellationToken cancellationToken);

        Task<GetById.Response> GetById(GetById.Request request, CancellationToken cancellationToken);
        Task<GetPaged.Response> GetPaged(GetPaged.Request request, CancellationToken cancellationToken);
    }
}