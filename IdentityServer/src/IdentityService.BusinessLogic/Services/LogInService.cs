using IdentityService.BusinessLogic.Exceptions;
using IdentityService.BusinessLogic.Services.Abstarct;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using System.Security.Claims;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace IdentityService.BusinessLogic.Services
{
    public class LogInService : ILogInService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public LogInService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ClaimsPrincipal> LogInAsync(AuthenticateResult result, OpenIddictRequest request)
        {
            var user = await _userManager.FindByNameAsync(result.Principal.Identity.Name);

            if (user != null)
            {
                var claims = new List<Claim>()
                {
                    new Claim(Claims.Subject, user.UserName),
                    new Claim(Claims.Email, user.Email).SetDestinations(Destinations.IdentityToken),
                    new Claim(Claims.Name, user.UserName),
                };

                var claimsIdentity = new ClaimsIdentity(claims, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

                var claimPrincipal = new ClaimsPrincipal(claimsIdentity);

                claimPrincipal.SetScopes(request.GetScopes());

                return await Task.FromResult(claimPrincipal);
            }

            throw new UserNotFoundException("User not found");
        }
    }
}
