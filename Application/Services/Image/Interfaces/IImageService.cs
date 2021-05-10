using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Services.Image.Contracts;

namespace SL2021.Application.Services.Image.Interfaces
{
    public interface IImageService
    {
        Task<Create.Response> Create(
            Create.Request request, 
            CancellationToken cancellationToken);
    }
}