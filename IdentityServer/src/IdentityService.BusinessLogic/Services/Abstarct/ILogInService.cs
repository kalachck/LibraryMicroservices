using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;
using System.Security.Claims;

namespace IdentityService.BusinessLogic.Services.Abstarct
{
    public interface ILogInService
    {
        Task<ClaimsPrincipal> LogInAsync(IdentityUser user, OpenIddictRequest request);
    }
}
