using AutoMapper;
using IdentityService.Api.Models;
using IdentityService.BusinessLogic.Providers.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> LogIn(LoginModel model)
        {
            var result = await _authorizationProvider.LogIn(_mapper.Map<IdentityUser>(model));

            if (result)
            {
                return Ok(new
                {
                    result.Value,
                });
            }

            return BadRequest(new
            {
                result.ExceptionMessage,
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
                Password = string.Empty,
            });
        }
    }
}
