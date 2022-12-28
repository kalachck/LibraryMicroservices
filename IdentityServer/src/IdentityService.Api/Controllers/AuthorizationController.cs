using IdentityService.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Controllers
{
    [Route("api/AuthorizationController")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        public AuthorizationController()
        { }

        [AllowAnonymous]
        [HttpPost]
        [Route("LogIn")]
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
        [Route("LogOut")]
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
