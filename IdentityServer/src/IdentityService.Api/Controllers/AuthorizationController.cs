using AutoMapper;
using IdentityService.Api.Models;
using IdentityService.BusinessLogic.Providers.Abstract;
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
        private readonly IAuthorizationProvider _authorizationProvider;

        private readonly IMapper _mapper;

        public AuthorizationController(IAuthorizationProvider authorizationProvider, IMapper mapper)
        {
            _authorizationProvider = authorizationProvider;

            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("LogIn")]
        public async Task<IActionResult> LogInAsync(LoginModel model)
        {
            var request = HttpContext.GetOpenIddictClientRequest();

            if (request == null)
            {
                return BadRequest("The OpenID Connect request cannot be retrieved.");
            }

            var result = await _authorizationProvider.LogInAsync(_mapper.Map<IdentityUser>(model), request);

            if (result)
            {
                return SignIn(result.Value, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }

            return BadRequest(new
            {
                result.ExceptionMessage,
            });
        }

        [Authorize]
        [HttpPost]
        [Route("LogOut")]
        public async Task<IActionResult> LogOutAsync(LoginModel model)
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
