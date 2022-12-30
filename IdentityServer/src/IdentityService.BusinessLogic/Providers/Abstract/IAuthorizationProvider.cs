using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;
using System.Security.Claims;

namespace IdentityService.BusinessLogic.Providers.Abstract
{
    public interface IAuthorizationProvider
    {
        Task<Result<ClaimsPrincipal>> LogInAsync(IdentityUser identityUser, OpenIddictRequest request);

        Task<Result<ClaimsPrincipal>> LogOutAsync(IdentityUser identityUser);
    }
}
