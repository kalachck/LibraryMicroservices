using Microsoft.AspNetCore.Identity;

namespace IdentityService.BusinessLogic.Services.Abstarct
{
    public interface IUserService
    {
        Task<IdentityUser> GetAsync(string email);

        Task<IdentityUser> AddAsync(IdentityUser user);

        Task<IdentityUser> UpdateAsync(string email, IdentityUser user);

        Task<IdentityUser> DeleteAsync(string email);

        Task<IdentityUser> UpdatePasswordAsync(string email, string oldPassword, string newPassword);

        Task<string> ResetPasswordAsync(string email);
    }
}
