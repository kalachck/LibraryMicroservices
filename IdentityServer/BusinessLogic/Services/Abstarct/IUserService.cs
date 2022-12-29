using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.Services.Abstarct
{
    public interface IUserService
    {
        Task<Result<IdentityUser>> GetAsync(Guid id);

        Task<Result<IdentityUser>> GetAsync(string email);

        Task<Result<IdentityUser>> AddAsync(IdentityUser user);

        Task<Result<IdentityUser>> UpdateAsync(Guid id, IdentityUser user);

        Task<Result<IdentityUser>> DeleteAsync(IdentityUser user);
    }
}
