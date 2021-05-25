using SL2021.Application.Identity.Interfaces;
using SL2021.Application.Repositories;
using SL2021.Application.Services.UserPic.Interfaces;

namespace SL2021.Application.Services.UserPic.Implementations
{
    public sealed partial class UserPicServiceV1 : IUserPicService
    {
        private readonly IUserRepository _userRepository;
        private readonly IIdentityService _identityService;
        private readonly IUserPicRepository _userPicRepository;
        public UserPicServiceV1(
            IUserRepository userRepository,
            IIdentityService identityService,
            IUserPicRepository userPicRepository) 
        {
            _userRepository = userRepository;
            _identityService = identityService;
            _userPicRepository = userPicRepository;
        }
    }
}