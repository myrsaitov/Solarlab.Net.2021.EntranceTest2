using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Services.WebLink.Contracts;

namespace SL2021.Application.Services.WebLink.Interfaces
{
    public interface IWebLinkService
    {
        Task<Create.Response> Create(
            Create.Request request,
            CancellationToken cancellationToken);
        Task<GetLinksFromPage.Response> GetLinksFromPage(
            GetLinksFromPage.Request request,
            CancellationToken cancellationToken);
    }
}