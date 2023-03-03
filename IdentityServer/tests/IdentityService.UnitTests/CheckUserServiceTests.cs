using AutoFixture;
using Grpc.Core.Testing;
using Grpc.Core;
using IdentityService.BusinessLogic.Services;
using IdentityService.BusinessLogic.Services.Abstarct;
using Microsoft.AspNetCore.Identity;
using Moq;
using IdentityService.BusinessLogic;
using FluentAssertions;
using IdentityService.BusinessLogic.Exceptions;

namespace IdentityService.UnitTests
{
    public class CheckUserServiceTests
    {
        private readonly Mock<IUserService> _userService;
        private readonly Fixture _fixture;

        public CheckUserServiceTests()
        {
            _userService = new Mock<IUserService>();
            _fixture = new Fixture();
        }

        [Fact]
        public async Task Check_ShouldReturnTrueUserResponse()
        {
            //Arrange
            _userService.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(_fixture.Create<IdentityUser>());

            var checkUserService = new CheckUserService(_userService.Object);

            var callContext = TestServerCallContext.Create(
                                             method: nameof(checkUserService.Check)
                                            , host: "localhost"
                                            , deadline: DateTime.Now.AddMinutes(30)
                                            , requestHeaders: new Metadata()
                                            , cancellationToken: CancellationToken.None
                                            , peer: "10.0.0.25:5001"
                                            , authContext: null
                                            , contextPropagationToken: null
                                            , writeHeadersFunc: (metadata) => Task.CompletedTask
                                            , writeOptionsGetter: () => new WriteOptions()
                                            , writeOptionsSetter: (writeOptions) => { }
                                            );

            //Act
            var actualResult = await checkUserService.Check(_fixture.Create<RequestEmail>(), callContext);

            //Assert
            actualResult.Should().BeOfType<UserResponse>();
            actualResult.IsExists.Should().BeTrue();
        }

        [Fact]
        public async Task Check_ShouldReturnFalseUserResponse()
        {
            //Arrange
            _userService.Setup(x => x.GetAsync(It.IsAny<string>())).Throws<NotFoundException>();

            var checkUserService = new CheckUserService(_userService.Object);

            var callContext = TestServerCallContext.Create(
                                             method: nameof(checkUserService.Check)
                                            , host: "localhost"
                                            , deadline: DateTime.Now.AddMinutes(30)
                                            , requestHeaders: new Metadata()
                                            , cancellationToken: CancellationToken.None
                                            , peer: "10.0.0.25:5001"
                                            , authContext: null
                                            , contextPropagationToken: null
                                            , writeHeadersFunc: (metadata) => Task.CompletedTask
                                            , writeOptionsGetter: () => new WriteOptions()
                                            , writeOptionsSetter: (writeOptions) => { }
                                            );

            //Act
            var actualResult = await checkUserService.Check(_fixture.Create<RequestEmail>(), callContext);

            //Assert
            actualResult.Should().BeOfType<UserResponse>();
            actualResult.IsExists.Should().BeFalse();
        }

        [Fact]
        public async Task Check_ShouldReturnException()
        {
            //Arrange
            _userService.Setup(x => x.GetAsync(It.IsAny<string>())).Throws<Exception>();

            var checkUserService = new CheckUserService(_userService.Object);

            var callContext = TestServerCallContext.Create(
                                             method: nameof(checkUserService.Check)
                                            , host: "localhost"
                                            , deadline: DateTime.Now.AddMinutes(30)
                                            , requestHeaders: new Metadata()
                                            , cancellationToken: CancellationToken.None
                                            , peer: "10.0.0.25:5001"
                                            , authContext: null
                                            , contextPropagationToken: null
                                            , writeHeadersFunc: (metadata) => Task.CompletedTask
                                            , writeOptionsGetter: () => new WriteOptions()
                                            , writeOptionsSetter: (writeOptions) => { }
                                            );

            //Act
            var act = () => checkUserService.Check(_fixture.Create<RequestEmail>(), callContext);

            //Assert
            await act.Should().ThrowAsync<Exception>();
        }
    }
}
