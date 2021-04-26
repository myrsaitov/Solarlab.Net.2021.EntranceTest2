using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Services.Category.Contracts;
using WidePictBoard.Application.Services.Contracts;

namespace WidePictBoard.Application.Services.Category.Interfaces
{
    public interface ICategoryService
    {
        Task<Create.Response> Create(
            Create.Request request, 
            CancellationToken cancellationToken);

        Task<Update.Response> Update(
            Update.Request request, 
            CancellationToken cancellationToken);

        Task Delete(
            Delete.Request request, 
            CancellationToken cancellationToken);

        Task Restore(
            Restore.Request request, 
            CancellationToken cancellationToken);

        Task<GetById.Response> GetById(
            GetById.Request request, 
            CancellationToken cancellationToken);

        Task<Paged.Response<GetById.Response>> GetPaged(
            Paged.Request request, 
            CancellationToken cancellationToken);
    }
}