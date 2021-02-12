using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WidePictBoard.Application.User.Interface;
using WidePictBoard.Domain.User;
using WidePictBoard.Infrastructure.Exceptions;

namespace WidePictBoard.Infrastructure.Repos
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Create(User user, string password, IList<string> roles, CancellationToken token)
        {
            user.CreatedOn = DateTime.UtcNow;
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded) return true;
            var errors = result.Errors.Select(error => (error.Code, error.Description)).ToList();
            throw new IdentityException(errors);
        }
        
        public async Task<IUser> FindById(string id, CancellationToken token)
        {
            throw new NotImplementedException();
        }
        
        public async Task<IUser> FindWhere(Expression<Func<IUser, bool>> query, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public async Task Save(IUser entity, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}