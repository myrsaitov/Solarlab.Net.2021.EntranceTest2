using MapsterMapper;
using SL2021.Application.Identity.Interfaces;
using SL2021.Application.Repositories;
using SL2021.Application.Services.Comment.Interfaces;

namespace SL2021.Application.Services.Comment.Implementations
{
    public sealed partial class CommentServiceV1 : ICommentService
    {
        private readonly IContentRepository _contentRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public CommentServiceV1(
            ICommentRepository commentRepository, 
            IContentRepository contentRepository, 
            IIdentityService identityService, 
            IMapper mapper)
        {
            _contentRepository = contentRepository;
            _commentRepository = commentRepository;
            _identityService = identityService;
            _mapper = mapper;
        }
    }
}