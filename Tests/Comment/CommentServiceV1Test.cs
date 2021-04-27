using SL2021.Application.Identity.Interfaces;
using SL2021.Application.Repositories;
using SL2021.Application.Services.Comment.Implementations;
using Moq;
using MapsterMapper;
using SL2021.Application.MapProfiles;
using Mapster;
using System.Linq.Expressions;

namespace SL2021.Tests.Comment
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

            TypeAdapterConfig.GlobalSettings.Compiler = exp => exp.CompileWithDebugInfo();
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
