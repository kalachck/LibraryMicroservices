using AutoMapper;
using IdentityService.Api.Models;
using IdentityService.BusinessLogic.Services.Abstarct;
using IdentityService.BusinessLogic.Validators.Abstract;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using System.Security.Claims;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace IdentityService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IValidator<LoginModel> _validator;

        public AuthorizationController(
            IAuthorizationService authorizationService,
            SignInManager<IdentityUser> signInManager,
            IMapper mapper,
            UserManager<IdentityUser> userManager,
            IValidator<LoginModel> validator)
        {
            _authorizationService = authorizationService;
            _signInManager = signInManager;
            _mapper = mapper;
            _userManager = userManager;
            _validator = validator;
        }

        [HttpGet]
        [HttpPost]
        [Route("LogIn")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> LogIn([FromQuery] LoginModel model)
        {
            await _validator.ValidateAsync(model);

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
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return SignOut(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        [HttpPost]
        [Route("Token")]
        [IgnoreAntiforgeryToken]
        [Produces("application/json")]
        public async Task<IActionResult> Exchange()
        {
            var request = HttpContext.GetOpenIddictServerRequest();

            if (request == null)
            {
                return BadRequest("The OpenID Connect request cannot be retrieved.");
            }

            if (request.ClientId == null)
            {
                throw new InvalidOperationException();
            }

            var result = await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

            var identityUser = await _userManager.FindByEmailAsync(result.Principal.GetClaim(Claims.Email));

            if (identityUser is null)
            {
                return Forbid(
                    authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                    properties: new AuthenticationProperties(new Dictionary<string, string>
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidGrant,
                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = "The token is no longer valid."
                    }));
            }

            ClaimsPrincipal claimsPrincipal;

            if (request.IsClientCredentialsGrantType())
            {
                claimsPrincipal = await _authorizationService.SetClaimsAsync(identityUser, request);
            }
            else if (request.IsAuthorizationCodeGrantType())
            {
                claimsPrincipal = result.Principal;
            }
            else if (request.IsRefreshTokenGrantType())
            {
                claimsPrincipal = result.Principal;
            }
            else
            {
                throw new InvalidOperationException("The specified grant type is not supported.");
            }

            return SignIn(claimsPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }
    }
}
