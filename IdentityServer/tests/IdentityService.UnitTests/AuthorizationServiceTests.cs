using AutoFixture;
using FluentAssertions;
using IdentityService.BusinessLogic.Exceptions;
using IdentityService.BusinessLogic.Services;
using Microsoft.AspNetCore.Identity;
using Moq;
using OpenIddict.Abstractions;
using System.Security.Claims;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace IdentityService.UnitTests
{
    public class AuthorizationServiceTests
    {
        private readonly Mock<UserManager<IdentityUser>> _userManager;
        private readonly Mock<OpenIddictRequest> _openIddictRequest;
        private readonly Fixture _fixture;

        public AuthorizationServiceTests()
        {
            _userManager = new Mock<UserManager<IdentityUser>>(new Mock<IUserStore<IdentityUser>>().Object,
                null, null, null, null, null, null, null, null);

            _userManager.Object.UserValidators.Add(new UserValidator<IdentityUser>());
            _userManager.Object.PasswordValidators.Add(new PasswordValidator<IdentityUser>());

            _openIddictRequest = new Mock<OpenIddictRequest>();
            _fixture = new Fixture();
        }

        [Fact]
        public async Task LogInAsync_ShouldReturnClaimsPrincipal()
        {
            //Arrange
            var user = _fixture.Create<IdentityUser>();

            var passwordHasher = new Mock<IPasswordHasher<IdentityUser>>();

            passwordHasher.Setup(x => x.VerifyHashedPassword(It.IsAny<IdentityUser>(), It.IsAny<string>(),
                It.IsAny<string>())).Returns(PasswordVerificationResult.Success);

            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
            _userManager.Setup(x => x.GetRolesAsync(It.IsAny<IdentityUser>())).ReturnsAsync(new List<string>());
            _userManager.Object.PasswordHasher = passwordHasher.Object;


            var authorizationService = new AuthorizationService(_userManager.Object);

            //Act
            var claimsPrincipal = await authorizationService.LogInAsync(user, _openIddictRequest.Object);

            //Assert
            claimsPrincipal.Should().NotBeNull();
            claimsPrincipal.Should().BeOfType<ClaimsPrincipal>();
            claimsPrincipal.GetClaim(Claims.Email).Should().Be(user.Email);
        }

        [Fact]
        public async Task LogInAsync_ShouldThrowInvalidPasswordException()
        {
            //Arrange
            var passwordHasher = new Mock<IPasswordHasher<IdentityUser>>();

            passwordHasher.Setup(x => x.VerifyHashedPassword(It.IsAny<IdentityUser>(), It.IsAny<string>(),
                It.IsAny<string>())).Returns(PasswordVerificationResult.Failed);

            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(_fixture.Create<IdentityUser>());
            _userManager.Object.PasswordHasher = passwordHasher.Object;

            var authorizationService = new AuthorizationService(_userManager.Object);

            //Act
            var act = () => authorizationService.LogInAsync(_fixture.Create<IdentityUser>(), _openIddictRequest.Object);

            //Assert
            await act.Should().ThrowAsync<InvalidPasswordException>("Invalid password"); 
        }

        [Fact]
        public async Task LogInAsync_ShouldThrowNotFoundException()
        {
            //Arrange
            _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((IdentityUser)null);

            var authorizationService = new AuthorizationService(_userManager.Object);

            //Act
            var act = () => authorizationService.LogInAsync(_fixture.Create<IdentityUser>(), _openIddictRequest.Object);

            //Assert
            await act.Should().ThrowAsync<NotFoundException>("User not found");
        }
    }
}
