using IdentityService.BusinessLogic.Exceptions;
using IdentityService.BusinessLogic.Services.Abstarct;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using System.Collections.Immutable;
using System.Security.Claims;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace IdentityService.BusinessLogic.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AuthorizationService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ClaimsPrincipal> LogInAsync(IdentityUser identityUser, OpenIddictRequest request)
        {
            var user = await _userManager.FindByEmailAsync(identityUser.Email);

            if (user != null)
            {
                var hashPassword = _userManager.PasswordHasher.HashPassword(identityUser, identityUser.PasswordHash);

                if (hashPassword == user.PasswordHash)
                {
                    var claimPrincipal = await SetClaimsAsync(user, request);
                
                    claimPrincipal.SetScopes(request.GetScopes());

                    return await Task.FromResult(claimPrincipal);
                }

                throw new InvalidPasswordException("Invalid password");
            }

            throw new NotFoundException("User not found");
        }

        public async Task<ClaimsPrincipal> SetClaimsAsync(IdentityUser identityUser, OpenIddictRequest request)
        {
            var claims = new List<Claim>()
            {
                new Claim(Claims.Subject, identityUser.Id).SetDestinations(Destinations.IdentityToken),
                new Claim(Claims.Email, identityUser.Email).SetDestinations(Destinations.AccessToken),
                new Claim(Claims.Name, identityUser.UserName).SetDestinations(Destinations.AccessToken),
            };

            var identity = new ClaimsIdentity(claims, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

            identity.AddClaims(Claims.Role, (await _userManager.GetRolesAsync(identityUser)).ToImmutableArray(), Destinations.AccessToken);

            var claimsPrincipal = new ClaimsPrincipal(identity);

            claimsPrincipal.SetScopes(request.GetScopes());

            return await Task.FromResult(claimsPrincipal);
        }
    }
}
