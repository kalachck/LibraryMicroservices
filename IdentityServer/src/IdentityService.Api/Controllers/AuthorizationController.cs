using AutoMapper;
using IdentityService.Api.Models;
using IdentityService.BusinessLogic.Services.Abstarct;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Server.AspNetCore;

namespace IdentityService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMapper _mapper;

        public AuthorizationController(
            IAuthorizationService authorizationService,
            SignInManager<IdentityUser> signInManager,
            IMapper mapper)
        {
            _authorizationService = authorizationService;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [HttpGet]
        [HttpPost]
        [Route("LogIn")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> LogIn([FromQuery] LoginModel model)
        {
            var request = HttpContext.GetOpenIddictServerRequest();

            if (request == null)
            {
                return BadRequest("The OpenID Connect request cannot be retrieved.");
            }

            var claimsPrincipal = await _authorizationService.LogInAsync(_mapper.Map<IdentityUser>(model), request);

            return SignIn(claimsPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        [HttpPost]
        [Route("LogOut")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return SignOut(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }
    }
}
