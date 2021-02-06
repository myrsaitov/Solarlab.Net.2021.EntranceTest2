using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WidePictBoard.Application.User.Contracts;
using WidePictBoard.Domain.User;


namespace WidePictBoard.Application.User.Service
{
    public class UserService : IUserService
    {
        public async Task<Register.Response> RegisterUser(Register.Request request, string returnUrl, 
            CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public async Task ConfirmEmail(string email, string returnUrl, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Login.Response> LoginUser(Login.Request request, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }
    }
}