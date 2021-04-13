using WidePictBoard.Application.Identity.Interfaces;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Application.Services.Tag.Implementations;
using Moq;
using MapsterMapper;
using WidePictBoard.Application.MapProfiles;
using WidePictBoard.Application.Services.PagedBase.Implementations;
using WidePictBoard.Application.Services.Tag.Contracts;

namespace WidePictBoard.Tests.Tag
{
    public partial class TagServiceV1Test
    {
        private Mock<ITagRepository> _tagRepositoryMock;
        private Mock<IIdentityService> _identityServiceMock;
        private Mock<PagedBase<GetById.Response, Domain.Tag, int>> _pagedMock;
        private IMapper _mapper;
        
        private TagServiceV1 _tagServiceV1;
        public TagServiceV1Test()
        {
            _tagRepositoryMock = new Mock<ITagRepository>();
            _pagedMock = new Mock<PagedBase<GetById.Response, Domain.Tag, int>>();

            _mapper = new Mapper();
            //ContentMapProfile.GetConfiguredMappingConfig().Compile();
            //CategoryMapProfile.GetConfiguredMappingConfig().Compile();
            ///CommentMapProfile.GetConfiguredMappingConfig().Compile();
            TagMapProfile.GetConfiguredMappingConfig().Compile();
            //UserMapProfile.GetConfiguredMappingConfig().Compile();

            _tagServiceV1 = new TagServiceV1(
                _tagRepositoryMock.Object,
                _mapper);
        }
    }
}
