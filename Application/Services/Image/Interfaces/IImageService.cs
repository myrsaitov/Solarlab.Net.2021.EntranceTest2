using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Services.Image.Contracts;

namespace SL2021.Application.Services.Image.Interfaces
{
    public interface IImageService
    {
        Task<UploadContents.Response> UploadContents(
            UploadContents.Request request, 
            CancellationToken cancellationToken);
        Task<UploadUser.Response> UploadUser(
            UploadUser.Request request,
            CancellationToken cancellationToken);
    }
}