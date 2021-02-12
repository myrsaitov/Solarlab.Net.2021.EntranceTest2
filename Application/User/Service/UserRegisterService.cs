using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WidePictBoard.Application.User.Contracts;
using WidePictBoard.Domain.User;


namespace WidePictBoard.Application.User.Service
{
    public partial class UserService
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
    }
}