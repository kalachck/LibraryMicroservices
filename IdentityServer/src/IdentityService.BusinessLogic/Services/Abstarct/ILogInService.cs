using Microsoft.AspNetCore.Authentication;
using OpenIddict.Abstractions;
using System.Security.Claims;

namespace IdentityService.BusinessLogic.Services.Abstarct
{
    public interface ILogInService
    {
        Task<ClaimsPrincipal> LogInAsync(AuthenticateResult result, OpenIddictRequest request);
    }
}
