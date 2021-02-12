using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using WidePictBoard.Application.User.Contracts;
using WidePictBoard.Application.User.Interface;
using WidePictBoard.Domain.User;


namespace WidePictBoard.Application.User.Service
{
    public partial class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Login.Response> LoginUser(Login.Request request, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }
    }
}