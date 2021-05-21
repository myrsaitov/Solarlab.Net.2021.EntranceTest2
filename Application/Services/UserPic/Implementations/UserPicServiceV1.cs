using SL2021.Application.Identity.Interfaces;
using SL2021.Application.Repositories;
using SL2021.Application.Services.UserPic.Interfaces;

namespace SL2021.Application.Services.UserPic.Implementations
{
    public sealed partial class UserPicServiceV1 : IUserPicService
    {
        private readonly IIdentityService _identityService;
        private readonly IUserPicRepository _userpicRepository;
        public UserPicServiceV1(
            IIdentityService identityService,
            IUserPicRepository userpicRepository) 
        {
            _identityService = identityService;
            _userpicRepository = userpicRepository;
        }
    }
}