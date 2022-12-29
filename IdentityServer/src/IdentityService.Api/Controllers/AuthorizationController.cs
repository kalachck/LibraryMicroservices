using BusinessLogic.Providers.Abstract;
using IdentityService.Api.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Server.AspNetCore;

namespace IdentityService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationProvider _authorizationProvider;

        public AuthorizationController(IAuthorizationProvider authorizationProvider)
        {
            _authorizationProvider = authorizationProvider;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("LogIn")]
        public async Task<IActionResult> LogIn(LoginModel model)
        {
            var request = HttpContext.GetOpenIddictServerRequest();

            if (request == null)
            {
                return BadRequest("The OpenID Connect request cannot be retrieved.");
            }

            var result = await HttpContext.AuthenticateAsync();

            var claimsPrincipal = await _authorizationProvider.LogIn(request, result);

            return SignIn(claimsPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        [Authorize]
        [HttpPost]
        [Route("LogOut")]
        public async Task<IActionResult> LogOut(LoginModel model)
        {
            await Task.Delay(2000);

            return Ok(new LoginModel()
            {
                UserName = string.Empty,
                Password = string.Empty,
            });
        }
    }
}
