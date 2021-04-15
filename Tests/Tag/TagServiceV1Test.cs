using WidePictBoard.Application.Identity.Interfaces;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Application.Services.Tag.Implementations;
using Moq;
using MapsterMapper;
using WidePictBoard.Application.MapProfiles;
using Mapster;
using System.Linq.Expressions;

namespace WidePictBoard.Tests.Tag
{
    public partial class TagServiceV1Test
    {
        private Mock<ITagRepository> _tagRepositoryMock;
        private IMapper _mapper;

        private TagServiceV1 _tagServiceV1;
        public TagServiceV1Test()
        {
            _tagRepositoryMock = new Mock<ITagRepository>();

            TypeAdapterConfig.GlobalSettings.Compiler = exp => exp.CompileWithDebugInfo();
            _mapper = new Mapper();
            TagMapProfile.GetConfiguredMappingConfig().Compile();

            _tagServiceV1 = new TagServiceV1(
                _tagRepositoryMock.Object,
                _mapper);
        }
    }
}
