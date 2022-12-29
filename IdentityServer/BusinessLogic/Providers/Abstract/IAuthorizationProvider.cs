using Microsoft.AspNetCore.Authentication;
using OpenIddict.Abstractions;
using System.Security.Claims;

namespace BusinessLogic.Providers.Abstract
{
    public interface IAuthorizationProvider
    {
        Task<ClaimsPrincipal> LogIn(OpenIddictRequest request, AuthenticateResult result);
    }
}
