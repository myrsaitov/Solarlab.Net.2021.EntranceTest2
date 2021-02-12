using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WidePictBoard.Application.User.Contracts;
using WidePictBoard.Application.User.Interface;

namespace WidePictBoard.Infrastructure.Services.User
{
    public partial class UserService : IUserService
    {
        private readonly UserManager<Infrastructure.User> _userManager;

        public UserService(UserManager<Infrastructure.User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Login.Response> LoginUser(Login.Request request, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }
    }
}