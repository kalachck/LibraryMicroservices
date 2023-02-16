using AutoFixture;
using AutoMapper;
using FluentAssertions;
using IdentityService.Api.Mappings;
using IdentityService.BusinessLogic.Exceptions;
using IdentityService.BusinessLogic.Services;
using IdentityService.BusinessLogic.Services.Abstarct;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace IdentityService.UnitTests
{
    public class UserServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<UserManager<IdentityUser>> _userManager;
        private readonly Mock<IMailService> _mailService;
        private readonly Fixture _fixture;

        public UserServiceTests()
        {
            _userManager = new Mock<UserManager<IdentityUser>>(new Mock<IUserStore<IdentityUser>>().Object,
                null, null, null, null, null, null, null, null);

            _userManager.Object.UserValidators.Add(new UserValidator<IdentityUser>());
            _userManager.Object.PasswordValidators.Add(new PasswordValidator<IdentityUser>());
            _userManager.Object.PasswordHasher = new PasswordHasher<IdentityUser>();

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<IdentityUserProfile>();
            });

            _mapper = new Mapper(configuration);
            _mailService = new Mock<IMailService>();
            _fixture = new Fixture();
        }

        [Fact]
        public async Task GetAsync_ShouldReturnIdentityUser()
        {
            //Arrange
            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(_fixture.Create<IdentityUser>());

            var userService = new UserService(_userManager.Object, _mailService.Object, _mapper);

            //Act
            var actualResult = await userService.GetAsync("testEmail");

            //Assert
            actualResult.Should().BeOfType<IdentityUser>();
            actualResult.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAsync_ShouldThrowNotFoundException()
        {
            //Arrange
            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((IdentityUser)null);

            var userService = new UserService(_userManager.Object, _mailService.Object, _mapper);

            //Act
            //Assert
            userService.Invoking(x => x.GetAsync("testEmail")).Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task GetAsync_ShoouldThrowsException()
        {
            //Arrange
            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Throws<Exception>();

            var userService = new UserService(_userManager.Object, _mailService.Object, _mapper);

            //Act
            //Assert
            userService.Invoking(x => x.GetAsync("testEmail")).Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task AddAsync_ShouldReturnSuccessfullMessage()
        {
            //Arrange
            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult((IdentityUser)null));
            _userManager.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>())).ReturnsAsync(IdentityResult.Success);

            var userService = new UserService(_userManager.Object, _mailService.Object, _mapper);

            //Act
            var actualResult = await userService.AddAsync(_fixture.Create<IdentityUser>());

            //Assert
            actualResult.Should().BeOfType<string>("The record was successfully added");
        }

        [Fact]
        public async Task AddAsync_ShouldThrowAlreadyExistsException()
        {
            //Arrange
            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(_fixture.Create<IdentityUser>());

            var userService = new UserService(_userManager.Object, _mailService.Object, _mapper);

            //Act
            //Assert
            userService.Invoking(x => x.AddAsync(_fixture.Create<IdentityUser>())).Should().ThrowAsync<AlreadyExistsException>();
        }

        [Fact]
        public async Task AddAsync_ShouldThrowException()
        {
            //Arrange
            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Throws<Exception>();

            var userService = new UserService(_userManager.Object, _mailService.Object, _mapper);

            //Act
            //Assert
            userService.Invoking(x => x.AddAsync(_fixture.Create<IdentityUser>())).Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnSuccessfullMessage()
        {
            //Arrange
            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(_fixture.Create<IdentityUser>());
            _userManager.Setup(x => x.UpdateAsync(It.IsAny<IdentityUser>())).ReturnsAsync(IdentityResult.Success);

            var userService = new UserService(_userManager.Object, _mailService.Object, _mapper);

            //Act
            var actualResult = await userService.UpdateAsync("testEmail", _fixture.Create<IdentityUser>());

            //Assert
            actualResult.Should().BeOfType<string>("The record was successfully updated");
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowNotFoundException()
        {
            //Arrange
            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((IdentityUser)null);

            var userService = new UserService(_userManager.Object, _mailService.Object, _mapper);

            //Act
            //Assert
            userService.Invoking(x => x.UpdateAsync("testEmail", _fixture.Create<IdentityUser>()))
                .Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException()
        {
            //Arrange
            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Throws<Exception>();

            var userService = new UserService(_userManager.Object, _mailService.Object, _mapper);

            //Act
            //Assert
            userService.Invoking(x => x.UpdateAsync("testEmail", _fixture.Create<IdentityUser>()))
                .Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnSuccessfullMessage()
        {
            //Arrange
            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(_fixture.Create<IdentityUser>());
            _userManager.Setup(x => x.DeleteAsync(It.IsAny<IdentityUser>())).ReturnsAsync(IdentityResult.Success);

            var userService = new UserService(_userManager.Object, _mailService.Object, _mapper);

            //Act
            var actualResult = await userService.DeleteAsync("testEmail");

            //Assert
            actualResult.Should().BeOfType<string>("The record was successfully deleted");
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowNotFoundException()
        {
            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((IdentityUser)null);

            var userService = new UserService(_userManager.Object, _mailService.Object, _mapper);

            //Act
            //Assert
            userService.Invoking(x => x.DeleteAsync("testEmail"))
                .Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowException()
        {
            //Arrange
            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Throws<Exception>();

            var userService = new UserService(_userManager.Object, _mailService.Object, _mapper);

            //Act
            //Assert
            userService.Invoking(x => x.DeleteAsync("testEmail")).Should().ThrowAsync<Exception>();
        }
    }
}
