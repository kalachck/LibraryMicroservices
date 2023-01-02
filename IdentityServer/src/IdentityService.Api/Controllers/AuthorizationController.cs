using IdentityService.BusinessLogic.Services.Abstarct;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Server.AspNetCore;

namespace IdentityService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly ILogInService _authorizationProvider;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthorizationController(ILogInService authorizationProvider,
            SignInManager<IdentityUser> signInManager)
        {
            _authorizationProvider = authorizationProvider;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("LogIn")]
        public async Task<IActionResult> LogIn()
        {
            var request = HttpContext.GetOpenIddictClientRequest();

            if (request == null)
            {
                return BadRequest("The OpenID Connect request cannot be retrieved.");
            }

            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (result.Succeeded)
            {
                return BadRequest("");
            }

            var claimsPrincipal = await _authorizationProvider.LogInAsync(result, request);

            return SignIn(claimsPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        [Authorize]
        [HttpPost]
        [Route("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return SignOut(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }
    }
}
