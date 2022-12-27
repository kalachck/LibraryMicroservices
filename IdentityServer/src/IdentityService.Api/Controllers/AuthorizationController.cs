using IdentityService.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        public AuthorizationController()
        { }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> LogIn(LoginModel model)
        {
            await Task.Delay(2000);

            return Ok(new LoginModel()
            {
                UserName = string.Empty,
                PasswordHash = string.Empty,
            });
        }

        [Authorize]
        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> LogOut(LoginModel model)
        {
            await Task.Delay(2000);

            return Ok(new LoginModel()
            {
                UserName = string.Empty,
                PasswordHash = string.Empty,
            });
        }
    }
}
