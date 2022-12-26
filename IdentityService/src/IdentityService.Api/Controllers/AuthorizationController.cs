using IdentityService.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class AuthorizationController : ControllerBase
    {
        public AuthorizationController()
        { }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync(LoginModel model)
        {
            throw new NotImplementedException();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> SignInAsync(LoginModel model)
        {
            throw new NotImplementedException();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SignOutAsync()
        {
            throw new NotImplementedException();
        }
    }
