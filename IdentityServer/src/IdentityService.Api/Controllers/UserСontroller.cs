using AutoMapper;
using IdentityService.Api.Models;
using IdentityService.BusinessLogic.Services.Abstarct;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserСontroller : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _autoMapper;

        public UserСontroller(IUserService userService, IMapper autoMapper)
        {
            _userService = userService;
            _autoMapper = autoMapper;
        }

        [HttpGet]
        [Route("Get/Email")]
        public async Task<IActionResult> Get(string email)
        {
            var user = await _userService.GetAsync(email);

            return Ok(user);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(LoginModel model)
        {
            var result = await _userService.AddAsync(_autoMapper.Map<IdentityUser>(model));

            return Ok(result);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(string email, LoginModel model)
        {
            var result = await _userService.UpdateAsync(email, _autoMapper.Map<IdentityUser>(model));

            return Ok(result);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(string email)
        {
            var result = await _userService.DeleteAsync(email);

            return Ok(result);
        }
    }
}
