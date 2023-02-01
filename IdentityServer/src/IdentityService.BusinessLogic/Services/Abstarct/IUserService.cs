using Microsoft.AspNetCore.Identity;

namespace IdentityService.BusinessLogic.Services.Abstarct
{
    public interface IUserService
    {
        Task<IdentityUser> GetAsync(string email);

        Task<string> AddAsync(IdentityUser user);

        Task<string> UpdateAsync(string email, IdentityUser user);

        Task<string> UpdatePasswordAsync(string email, string oldPassword, string newPassword);

        Task<string> ResetPasswordAsync(string email);

        Task<string> DeleteAsync(string email);
    }
}
