using AutoMapper;
using IdentityService.Api.Models;
using IdentityService.BusinessLogic.Services.Abstarct;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        [Route("Get/Id")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _userService.GetAsync(id);

            if (result)
            {
                return Ok(new
                {
                    result.Value,
                    result.Message,
                });
            }

            return BadRequest(result.ExceptionMessage);
        }

        [HttpGet]
        [Route("Get/Email")]
        public async Task<IActionResult> Get(string email)
        {
            var result = await _userService.GetAsync(email);

            if (result)
            {
                return Ok(new
                {
                    result.Value,
                    result.Message,
                });
            }

            return BadRequest(result.ExceptionMessage);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(LoginModel model)
        {
            var result = await _userService.AddAsync(_autoMapper.Map<IdentityUser>(model));

            if (result)
            {
                return Ok(new
                {
                    result.Value,
                    result.Message,
                });
            }

            return BadRequest(result.ExceptionMessage);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(Guid id, LoginModel model)
        {
            var result = await _userService.UpdateAsync(id, _autoMapper.Map<IdentityUser>(model));

            if (result)
            {
                return Ok(new
                {
                    result.Value,
                    result.Message,
                });
            }

            return BadRequest(result.ExceptionMessage);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var resultFromGet = await _userService.GetAsync(id);

            if (resultFromGet)
            {
                var result = await _userService.DeleteAsync(resultFromGet.Value);

                if (result)
                {
                    return Ok(new
                    {
                        result.Value,
                        result.Message,
                    });
                }

                return BadRequest(result.ExceptionMessage);
            }

            return BadRequest(resultFromGet.ExceptionMessage);
        }
    }
}
