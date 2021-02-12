using System;
using Mapster;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WidePictBoard.Application.User.Contracts;

namespace WidePictBoard.Infrastructure.Services.User
{
    public partial class UserService
    {
        
        public async Task<Register.Response> RegisterUser(Register.Request request, string returnUrl, 
            CancellationToken token)
        {
            var user = request.Adapt<Infrastructure.User>();
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded) return new Register.Response();
            var errors = result.Errors.Select(error => (error.Code, error.Description)).ToList();
            throw new IdentityException(errors);
        }

        public async Task ConfirmEmail(string email, string returnUrl, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }
    }
}