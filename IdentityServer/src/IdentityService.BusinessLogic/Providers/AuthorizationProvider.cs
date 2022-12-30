using IdentityService.BusinessLogic.Providers.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using System.Security.Claims;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace IdentityService.BusinessLogic.Providers
{
    public class AuthorizationProvider : IAuthorizationProvider
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AuthorizationProvider(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<ClaimsPrincipal>> LogInAsync(IdentityUser identityUser, OpenIddictRequest request)
        {
            var user = await _userManager.FindByEmailAsync(identityUser.Email);

            if (user != null)
            {
                var claims = new List<Claim>()
                {
                    new Claim(Claims.Subject, user.Id),
                    new Claim(Claims.Email, user.Email),
                    new Claim(Claims.Name, user.UserName),
                    //new Claim(Claims.Role, (await _userManager.GetRolesAsync(user)).ToImmutableArray()),
                };

                var claimsIdentity = new ClaimsIdentity(claims, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

                var claimPrincipal = new ClaimsPrincipal(claimsIdentity);

                claimPrincipal.SetScopes(request.GetScopes());

                return await Task.FromResult(new Result<ClaimsPrincipal>()
                {
                    Value = claimPrincipal,
                });
            }

            return await Task.FromResult(new Result<ClaimsPrincipal>()
            {
                ExceptionMessage = "User doesn't exist"
            });
        }

        //This logic has not implemented yet
        public Task<Result<ClaimsPrincipal>> LogOutAsync(IdentityUser identityUser)
        {
            throw new NotImplementedException();
        }
    }
}
