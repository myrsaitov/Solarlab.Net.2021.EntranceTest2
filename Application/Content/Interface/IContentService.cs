using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.Content.Contracts;

namespace WidePictBoard.Application.Content.Interface
{
    public interface IContentService
    {
        Task<Create.Response> Create(Create.Request request, CancellationToken cancellationToken);

        Task<GetById.Response> GetById(GetById.Request request, CancellationToken cancellationToken);

        Task Delete(Delete.Request request, CancellationToken cancellationToken);

        
        Task<GetPaged.Response> GetPaged(GetPaged.Request request, CancellationToken cancellationToken);



        /*
        Task<GetById.Response> GetById(GetById.Request request);
        //Task<GetPaged.Response> GetPaged(GetPaged.Request request);
        Task<Delete.Response> Delete(Delete.Request request);
        Task<Create.Response> Create(Create.Request request);
       // Task<Update.Response> Update(Update.Request request); */
    }
}