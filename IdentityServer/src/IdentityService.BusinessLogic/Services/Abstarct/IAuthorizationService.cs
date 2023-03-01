using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;
using System.Security.Claims;

namespace IdentityService.BusinessLogic.Services.Abstarct
{
    public interface IAuthorizationService
    {
        Task<ClaimsPrincipal> LogInAsync(IdentityUser identityUser, OpenIddictRequest request);

        Task<ClaimsPrincipal> SetClaimsAsync(IdentityUser identityUser, OpenIddictRequest request);
    }
}
