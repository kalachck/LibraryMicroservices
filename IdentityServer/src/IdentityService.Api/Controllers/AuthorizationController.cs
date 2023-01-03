using AutoMapper;
using IdentityService.Api.Models;
using IdentityService.BusinessLogic.Services.Abstarct;
using Microsoft.AspNetCore;
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
        private readonly IMapper _mapper;

        public AuthorizationController(
            ILogInService authorizationProvider,
            SignInManager<IdentityUser> signInManager,
            IMapper mapper)
        {
            _authorizationProvider = authorizationProvider;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("LogIn")]
        public async Task<IActionResult> LogIn(LoginModel model)
        {
            var request = HttpContext.GetOpenIddictClientRequest();

            if (request == null)
            {
                return BadRequest("The OpenID Connect request cannot be retrieved.");
            }

            var claimsPrincipal = await _authorizationProvider.LogInAsync(_mapper.Map<IdentityUser>(model), request);

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
