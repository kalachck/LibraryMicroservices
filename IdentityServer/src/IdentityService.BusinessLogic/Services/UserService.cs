using IdentityService.BusinessLogic.Exceptions;
using IdentityService.BusinessLogic.Services.Abstarct;
using Microsoft.AspNetCore.Identity;

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
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                return await Task.FromResult(user);
            }

            throw new NotFoundException("User not found");
        }

        public async Task<IdentityUser> AddAsync(IdentityUser identityUser)
        {
            if (await _userManager.FindByEmailAsync(identityUser.Email) == null)
            {
                identityUser.PasswordHash = _userManager.PasswordHasher.HashPassword(identityUser, identityUser.PasswordHash);

                await _userManager.CreateAsync(identityUser);

                return await Task.FromResult(identityUser);
            }

<<<<<<< HEAD
            throw new UserAlreadyExistsException("This user already exists");
=======
            throw new AlreadyExistsException("This user already exists");
>>>>>>> BLL,DALAndApiImplemantation
        }

        public async Task<IdentityUser> UpdateAsync(string email, IdentityUser identityUser)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                identityUser.PasswordHash = _userManager.PasswordHasher.HashPassword(identityUser, identityUser.PasswordHash);

                user.UserName = identityUser.UserName;
                user.Email = identityUser.Email;
                user.PasswordHash = identityUser.PasswordHash;

                await _userManager.UpdateAsync(user);

                return await Task.FromResult(user);
            }

            throw new NotFoundException("User not found");
        }

        public async Task<IdentityUser> DeleteAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                await _userManager.DeleteAsync(user);

                return await Task.FromResult(user);
            }

            throw new NotFoundException("User not found");
        }
    }
}
