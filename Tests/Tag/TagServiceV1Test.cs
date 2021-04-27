using SL2021.Application.Identity.Interfaces;
using SL2021.Application.Repositories;
using SL2021.Application.Services.Tag.Implementations;
using Moq;
using MapsterMapper;
using SL2021.Application.MapProfiles;
using Mapster;
using System.Linq.Expressions;

namespace SL2021.Tests.Tag
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
