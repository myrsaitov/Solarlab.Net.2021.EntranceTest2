using SL2021.Application.Identity.Interfaces;
using SL2021.Application.Repositories;
using SL2021.Application.Services.User.Interfaces;
using MapsterMapper;

namespace SL2021.Application.Services.User.Implementations
{
    public sealed partial class UserServiceV1 : IUserService
    {
        private readonly IRepository<Domain.User, string> _repository;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;
        public UserServiceV1(
            IRepository<Domain.User, string> repository, 
            IIdentityService identityService,
            IMapper mapper)
        {
            _repository = repository;
            _identityService = identityService;
            _mapper = mapper;
        }
    }
}