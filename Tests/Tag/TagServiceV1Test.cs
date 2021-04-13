using WidePictBoard.Application.Identity.Interfaces;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Application.Services.Category.Implementations;
using Moq;
using MapsterMapper;
using WidePictBoard.Application.MapProfiles;
using WidePictBoard.Application.Services.PagedBase.Implementations;
using WidePictBoard.Application.Services.Tag.Contracts;

namespace WidePictBoard.Tests.Tag
{
    public partial class TagServiceV1Test
    {
        private Mock<ITagRepository> _categoryRepositoryMock;
        private Mock<ITagRepository> _tagRepositoryMock;
        private Mock<IIdentityService> _identityServiceMock;
        private Mock<PagedBase<GetById.Response, Domain.Tag, int>> _pagedMock;
        private IMapper _mapper;
        
        private TagServiceV1 _categoryServiceV1;
        public TagServiceV1Test()
        {
            _categoryRepositoryMock = new Mock<ITagRepository>();
            _tagRepositoryMock = new Mock<ITagRepository>();
            _identityServiceMock = new Mock<IIdentityService>();
            _pagedMock = new Mock<PagedBase<GetById.Response, Domain.Tag, int>>();

            _mapper = new Mapper();
            ContentMapProfile.GetConfiguredMappingConfig().Compile();
            TagMapProfile.GetConfiguredMappingConfig().Compile();
            CommentMapProfile.GetConfiguredMappingConfig().Compile();
            TagMapProfile.GetConfiguredMappingConfig().Compile();
            UserMapProfile.GetConfiguredMappingConfig().Compile();

            _categoryServiceV1 = new TagServiceV1(
                _categoryRepositoryMock.Object,
                _identityServiceMock.Object,
                _mapper);
        }
    }
}
