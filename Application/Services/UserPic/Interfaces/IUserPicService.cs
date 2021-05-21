using System.Threading;
using System.Threading.Tasks;
using SL2021.Application.Services.UserPic.Contracts;

namespace SL2021.Application.Services.UserPic.Interfaces
{
    public interface IUserPicService
    {
        Task<Create.Response> Create(
            Create.Request request,
            CancellationToken cancellationToken);
    }
}