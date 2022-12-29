using BusinessLogic.Providers.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using OpenIddict.Abstractions;
using System.Collections.Immutable;
using System.Net;
using System.Security.Claims;
using static OpenIddict.Abstractions.OpenIddictConstants;
using static System.Net.Mime.MediaTypeNames;

namespace BusinessLogic.Providers
{
    public class AuthorizationProvider : IAuthorizationProvider
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AuthorizationProvider(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<string>> LogIn(IdentityUser identityUser)
        {
            var user = await _userManager.FindByEmailAsync(identityUser.Email);

            if (user != null)
            {
                var identity = new ClaimsIdentity();

                identity.SetClaim(Claims.Subject, await _userManager.GetUserIdAsync(user))
                        .SetClaim(Claims.Email, await _userManager.GetEmailAsync(user))
                        .SetClaim(Claims.Name, await _userManager.GetUserNameAsync(user))
                        .SetClaims(Claims.Role, (await _userManager.GetRolesAsync(user)).ToImmutableArray());


                //There must be token generation logic, let's imagine that there is one here
                var token = "token";

                return await Task.FromResult(new Result<string>()
                {
                    Value = token
                });
            }

            return await Task.FromResult(new Result<string>()
            {
                ExceptionMessage = "User doesn't exist"
            });
        }
    }
}
