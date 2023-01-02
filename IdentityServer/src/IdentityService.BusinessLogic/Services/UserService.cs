using IdentityService.BusinessLogic.Exceptions;
using IdentityService.BusinessLogic.Services.Abstarct;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace IdentityService.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityUser> GetAsync(string email)
        {
            var user =  await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                return await Task.FromResult(user);
            }

            throw new UserNotFoundException("User not found");
        }

        public async Task<IdentityUser> AddAsync(IdentityUser identityUser)
        {
            var user = await _userManager.FindByEmailAsync(identityUser.Email);

            if (user == null)
            {
                await _userManager.CreateAsync(identityUser);

                return await Task.FromResult(identityUser);
            }

            throw new UserAlreadyExists("This user already exists");
        }

        public async Task<IdentityUser> UpdateAsync(string email, IdentityUser identityUser)
        {
            if (await _userManager.FindByEmailAsync(email) != null)
            {
                await _userManager.UpdateAsync(identityUser);

                return await Task.FromResult(identityUser);
            }

            throw new UserNotFoundException("User not found");
        }
        
        public async Task<IdentityUser> DeleteAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                await _userManager.DeleteAsync(user);

                return await Task.FromResult(user);
            }

            throw new UserNotFoundException("User not found");
        }
    }
}
