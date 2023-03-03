using AutoFixture;
using AutoMapper;
using FluentAssertions;
using IdentityService.Api.Mappings;
using IdentityService.BusinessLogic.Exceptions;
using IdentityService.BusinessLogic.Services;
using IdentityService.BusinessLogic.Services.Abstarct;
using k8s.KubeConfigModels;
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
            var act = () => userService.GetAsync("testEMail");

            //Assert
            await act.Should().ThrowAsync<NotFoundException>("User not found");
        }

        [Fact]
        public async Task GetAsync_ShoouldThrowsException()
        {
            //Arrange
            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Throws<Exception>();

            var userService = new UserService(_userManager.Object, _mailService.Object, _mapper);

            //Act
            var act = () => userService.GetAsync("testEMail");

            //Assert
            await act.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task AddAsync_ShouldReturnSuccessfullMessage()
        {
            //Arrange
            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns(Task.FromResult((IdentityUser)null));
            _userManager.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>())).ReturnsAsync(IdentityResult.Success);
            _userManager.Object.PasswordHasher = new PasswordHasher<IdentityUser>();

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
            var act = () => userService.AddAsync(_fixture.Create<IdentityUser>());

            //Assert
            await act.Should().ThrowAsync<AlreadyExistsException>("This user already exists");
        }

        [Fact]
        public async Task AddAsync_ShouldThrowException()
        {
            //Arrange
            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Throws<Exception>();

            var userService = new UserService(_userManager.Object, _mailService.Object, _mapper);

            //Act
            var act = () => userService.AddAsync(_fixture.Create<IdentityUser>());

            //Assert
            await act.Should().ThrowAsync<Exception>();
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
            var act = () => userService.UpdateAsync("testEmail", _fixture.Create<IdentityUser>());

            //Assert
            await act.Should().ThrowAsync<NotFoundException>("User not found");
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException()
        {
            //Arrange
            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Throws<Exception>();

            var userService = new UserService(_userManager.Object, _mailService.Object, _mapper);

            //Act
            var act = () => userService.UpdateAsync("testEmail", _fixture.Create<IdentityUser>());

            //Assert
            await act.Should().ThrowAsync<Exception>();
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
            var act = () => userService.DeleteAsync("testEmail");

            //Assert
            await act.Should().ThrowAsync<NotFoundException>("User not found");
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowException()
        {
            //Arrange
            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Throws<Exception>();

            var userService = new UserService(_userManager.Object, _mailService.Object, _mapper);

            //Act
            var act = () => userService.DeleteAsync("testEmail");

            //Assert
            await act.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task UpdatePasswordAsync_ShouldReturnSuccessfullMessage()
        {
            //Arrange
            var passwordHasher = new Mock<IPasswordHasher<IdentityUser>>();

            passwordHasher.Setup(x => x.VerifyHashedPassword(It.IsAny<IdentityUser>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(PasswordVerificationResult.Success);

            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(_fixture.Create<IdentityUser>());
            _userManager.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>())).ReturnsAsync(IdentityResult.Success);
            _userManager.Object.PasswordHasher = passwordHasher.Object;

            var userService = new UserService(_userManager.Object, _mailService.Object, _mapper);

            //Act
            var actualResult = await userService.UpdatePasswordAsync("testEmail", "currentPassword", "newPassword");

            //Assert
            actualResult.Should().NotBeNullOrWhiteSpace().And.BeOfType<string>("Password was successfully updated");
        }

        [Fact]
        public async Task UpdatePasswordAsync_ShouldThrowInvalidPasswordException()
        {
            //Arrange
            var passwordHasher = new Mock<IPasswordHasher<IdentityUser>>();

            passwordHasher.Setup(x => x.VerifyHashedPassword(It.IsAny<IdentityUser>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(PasswordVerificationResult.Failed);

            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(_fixture.Create<IdentityUser>());
            _userManager.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>())).ReturnsAsync(IdentityResult.Success);
            _userManager.Object.PasswordHasher = passwordHasher.Object;

            var userService = new UserService(_userManager.Object, _mailService.Object, _mapper);

            //Act
            var act = () => userService.UpdatePasswordAsync("testEmail", "currentPassword", "newPassword");

            //Assert
            await act.Should().ThrowAsync<InvalidPasswordException>("Current password doesn't match with the typed one");
        }

        [Fact]
        public async Task UpdatePasswordAsync_ShouldThrowNotFoundException()
        {
            //Arrange
            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((IdentityUser)null);

            var userService = new UserService(_userManager.Object, _mailService.Object, _mapper);

            //Act
            var act = () => userService.UpdatePasswordAsync("testEmail", "currentPassword", "newPassword");

            //Assert
            await act.Should().ThrowAsync<NotFoundException>("User with this email doesn't exists");
        }

        [Fact]
        public async Task ReserPasswordAsync_ShouldReturnResetCode()
        {
            //Arrange
            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(_fixture.Create<IdentityUser>());
            _mailService.Setup(x => x.SendMessageAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()));

            var userService = new UserService(_userManager.Object, _mailService.Object, _mapper);

            //Act
            var actualResult = await userService.ResetPasswordAsync("testEmail");

            //Assert
            actualResult.Should().BeOfType<string>().And.NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task ReserPasswordAsync_ShouldThrowNotFoundException()
        {
            //Arrange
            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((IdentityUser)null);

            var userService = new UserService(_userManager.Object, _mailService.Object, _mapper);

            //Act
            var act = () => userService.ResetPasswordAsync("testEmail");

            //Assert
            await act.Should().ThrowAsync<NotFoundException>("User with this email doesn't exist");
        }
    }
}
