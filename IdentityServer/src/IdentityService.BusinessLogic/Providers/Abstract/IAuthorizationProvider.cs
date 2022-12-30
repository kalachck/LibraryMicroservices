using Microsoft.AspNetCore.Identity;

namespace IdentityService.BusinessLogic.Providers.Abstract
{
    public interface IAuthorizationProvider
    {
        Task<Result<string>> LogIn(IdentityUser identityUser);

        Task<Result<string>> LogOut(IdentityUser identityUser);
    }
}
