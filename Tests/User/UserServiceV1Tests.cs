using WidePictBoard.Application.Identity.Contracts;
using WidePictBoard.Application.Identity.Interfaces;
using WidePictBoard.Application.MapProfiles;
using WidePictBoard.Application.Repositories;
using WidePictBoard.Application.Services.User.Contracts;
using WidePictBoard.Application.Services.User.Contracts.Exceptions;
using WidePictBoard.Application.Services.User.Implementations;
using AutoFixture;
using AutoFixture.Xunit2;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using MapsterMapper;
using Xunit;

namespace WidePictBoard.Application.Tests.User
{
    public class UserServiceV1Tests
    {
        private Mock<IRepository<Domain.User, string>> _repositoryMock;
        private Mock<IIdentityService> _identityServiceMock;
        private IMapper _mapper;

        private UserServiceV1 _userServiceV1;

        public UserServiceV1Tests()
        {
            _repositoryMock = new Mock<IRepository<Domain.User, string>>();
            _identityServiceMock = new Mock<IIdentityService>();

            _mapper = new Mapper();
            UserMapProfile.GetConfiguredMappingConfig().Compile();

            _userServiceV1 = new UserServiceV1(_repositoryMock.Object, _identityServiceMock.Object, _mapper);
        }

        [Theory]
        [AutoData]
        public async Task Register_Returns_Response_Success(
            Register.Request registerRequest, CancellationToken cancellationToken)
        {
            // Arrange
            registerRequest.Email = "sysd@mail.ru";
            var createUserResponse = new Fixture()
                .Build<CreateUser.Response>()
                .Do(_ => _.IsSuccess = true)
                .Create();

            _identityServiceMock
                .Setup(_ => _.CreateUser(It.IsAny<CreateUser.Request>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(createUserResponse)
                .Verifiable();

            // Act
            Register.Response registerResponse = await _userServiceV1.Register(registerRequest, cancellationToken);

            // Assert
            _identityServiceMock.Verify();
            Assert.NotNull(registerResponse);
            Assert.NotEqual(default, registerResponse.UserId);
        }

        [Theory]
        [AutoData]
        public async Task Register_Throws_Exception_When_Request_Is_Invalid(
            Register.Request registerRequest, CancellationToken cancellationToken)
        {
            var createUserResponse = new Fixture()
                .Build<CreateUser.Response>()
                .Do(_ => _.IsSuccess = true)
                .Create();

            _identityServiceMock
                .Setup(_ => _.CreateUser(It.IsAny<CreateUser.Request>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(createUserResponse);

            await Assert.ThrowsAsync<UserRegisteredException>(
                async () => await _userServiceV1.Register(registerRequest, cancellationToken));
        }
    }
}
