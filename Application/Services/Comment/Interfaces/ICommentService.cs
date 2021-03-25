using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Services.Comment.Contracts;

namespace WidePictBoard.Application.Services.Comment.Interfaces
{
    public interface ICommentService
    {
        Task<Create.Response> Create(Create.Request request, CancellationToken cancellationToken);


        Task<GetById.Response> GetById(GetById.Request request, CancellationToken cancellationToken);
        Task<GetAll.Response> GetPaged(CancellationToken cancellationToken);
    }

}