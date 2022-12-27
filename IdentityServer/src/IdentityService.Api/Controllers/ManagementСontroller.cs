using IdentityService.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ManagementСontroller : ControllerBase
    {
        public ManagementСontroller()
        { }

        [HttpPost]
        public async Task<IActionResult> Add(LoginModel model)
        {
            await Task.Delay(2000);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, LoginModel model)
        {
            await Task.Delay(2000);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await Task.Delay(2000);

            return Ok();
        }
    }
}
