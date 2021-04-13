using WidePictBoard.Application.Identity.Interfaces;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Application.Services.Comment.Implementations;
using Moq;
using MapsterMapper;
using WidePictBoard.Application.MapProfiles;

namespace WidePictBoard.Tests.Comment
{
    public partial class CommentServiceV1Test
    {
        private Mock<ICommentRepository> _commentRepositoryMock;
        private Mock<IContentRepository> _contentRepositoryMock;
        private Mock<IIdentityService> _identityServiceMock;
        private IMapper _mapper;
        
        private CommentServiceV1 _commentServiceV1;
        public CommentServiceV1Test()
        {
            _commentRepositoryMock = new Mock<ICommentRepository>();
            _contentRepositoryMock = new Mock<IContentRepository>();
            _identityServiceMock = new Mock<IIdentityService>();
           
            _mapper = new Mapper();
            ContentMapProfile.GetConfiguredMappingConfig().Compile();
            CommentMapProfile.GetConfiguredMappingConfig().Compile();
            UserMapProfile.GetConfiguredMappingConfig().Compile();

            _commentServiceV1 = new CommentServiceV1(
                _commentRepositoryMock.Object,
                _contentRepositoryMock.Object, 
                _identityServiceMock.Object,
                _mapper);
        }
    }
}
