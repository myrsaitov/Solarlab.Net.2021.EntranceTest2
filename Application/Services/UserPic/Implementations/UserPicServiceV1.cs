using SL2021.Application.Identity.Interfaces;
using SL2021.Application.Repositories;
using SL2021.Application.Services.UserPic.Interfaces;

namespace SL2021.Application.Services.UserPic.Implementations
{
    public sealed partial class UserPicServiceV1 : IUserPicService
    {
        private readonly IUserRepository _userRepository;
        private readonly IIdentityService _identityService;
        private readonly IUserPicRepository _userpicRepository;
        public UserPicServiceV1(
            IUserRepository userRepository,
            IIdentityService identityService,
            IUserPicRepository userpicRepository) 
        {
            _userRepository = userRepository;
            _identityService = identityService;
            _userpicRepository = userpicRepository;
        }
    }
}