using BusinessLogic.Providers.Abstract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using System.Collections.Immutable;
using System.Security.Claims;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace BusinessLogic.Providers
{
    public class AuthorizationProvider : IAuthorizationProvider
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AuthorizationProvider(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ClaimsPrincipal> LogIn(OpenIddictRequest request, AuthenticateResult result)
        {
            var user = await _userManager.GetUserAsync(result.Principal);

            var identity = new ClaimsIdentity(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

            identity.SetClaim(Claims.Subject, await _userManager.GetUserIdAsync(user))
                        .SetClaim(Claims.Email, await _userManager.GetEmailAsync(user))
                        .SetClaim(Claims.Name, await _userManager.GetUserNameAsync(user))
                        .SetClaims(Claims.Role, (await _userManager.GetRolesAsync(user)).ToImmutableArray());


            identity.SetDestinations(_ => new[] { Destinations.AccessToken });

            var claimsPrincipal = new ClaimsPrincipal(identity);

            claimsPrincipal.SetScopes(request.GetScopes());

            return await Task.FromResult(claimsPrincipal);
        }
    }
}
