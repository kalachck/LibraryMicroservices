using IdentityService.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Controllers
{
    [Route("api/UserController")]
    [ApiController]
    [Authorize]
    public class UserСontroller : ControllerBase
    {
        public UserСontroller()
        { }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(LoginModel model)
        {
            await Task.Delay(2000);

            return Ok(new LoginModel()
            {
                UserName = string.Empty,
                PasswordHash = string.Empty,
            });
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(int id, LoginModel model)
        {
            await Task.Delay(2000);

            return Ok(new LoginModel()
            {
                UserName = string.Empty,
                PasswordHash = string.Empty,
            });
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
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
