using IdentityService.BusinessLogic.Exceptions;
using IdentityService.BusinessLogic.Services.Abstarct;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
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

            var hashPassword = _userManager.PasswordHasher.HashPassword(identityUser, identityUser.PasswordHash);

            if (user != null)
            {
                if (hashPassword == user.PasswordHash)
                {
<<<<<<< HEAD:IdentityServer/src/IdentityService.BusinessLogic/Services/LogInService.cs
                    var claims = new List<Claim>()
                    {
                        new Claim(Claims.Subject, identityUser.UserName),
                        new Claim(Claims.Email, identityUser.Email).SetDestinations(Destinations.IdentityToken),
                        new Claim(Claims.Name, identityUser.UserName),
                    };
=======
                    new Claim(Claims.Subject, identityUser.UserName),
                    new Claim(Claims.Email, identityUser.Email).SetDestinations(Destinations.IdentityToken),
                };
>>>>>>> BLL,DALAndApiImplemantation:IdentityServer/src/IdentityService.BusinessLogic/Services/AuthorizationService.cs

                    var claimsIdentity = new ClaimsIdentity(claims, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

                    var claimPrincipal = new ClaimsPrincipal(claimsIdentity);

                    claimPrincipal.SetScopes(request.GetScopes());

                    return await Task.FromResult(claimPrincipal);
                }

                throw new InvalidPasswordException("Invalid password");
            }

            throw new NotFoundException("User not found");
        }
    }
}
