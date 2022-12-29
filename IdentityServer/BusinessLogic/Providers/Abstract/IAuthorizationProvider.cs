using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.Providers.Abstract
{
    public interface IAuthorizationProvider
    {
        Task<Result<string>> LogIn(IdentityUser identityUser);
    }
}
