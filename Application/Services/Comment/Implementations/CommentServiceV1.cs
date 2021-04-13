using MapsterMapper;
using WidePictBoard.Application.Identity.Interfaces;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Application.Services.Comment.Interfaces;

namespace WidePictBoard.Application.Services.Comment.Implementations
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