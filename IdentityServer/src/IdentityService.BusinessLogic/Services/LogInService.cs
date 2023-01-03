using IdentityService.BusinessLogic.Exceptions;
using IdentityService.BusinessLogic.Services.Abstarct;
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

        public async Task<ClaimsPrincipal> LogInAsync(IdentityUser identityUser, OpenIddictRequest request)
        {
            if (await _userManager.FindByEmailAsync(identityUser.Email) != null)
            {
                var claims = new List<Claim>()
                {
                    new Claim(Claims.Subject, identityUser.UserName),
                    new Claim(Claims.Email, identityUser.Email).SetDestinations(Destinations.IdentityToken),
                    new Claim(Claims.Name, identityUser.UserName),
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
