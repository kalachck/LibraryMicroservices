using BusinessLogic.Services.Abstarct;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<IdentityUser>> GetAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user is not null)
            {
                return await Task.FromResult(new Result<IdentityUser>()
                {
                    Value = user,
                    Message = "User was found successfully",
                });
            }

            return await Task.FromResult(new Result<IdentityUser>()
            {
                ExceptionMessage = "User not found",
                Message = "User was found successfully",
            });;
        }

        public async Task<Result<IdentityUser>> GetAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is not null)
            {
                return await Task.FromResult(new Result<IdentityUser>()
                {
                    Value = user,
                    Message = "User was found successfully",
                });
            }

            return await Task.FromResult(new Result<IdentityUser>() 
            {
                ExceptionMessage = "User was not found"
            });
        }

        public async Task<Result<IdentityUser>> AddAsync(IdentityUser identityUser)
        {
            var user = await _userManager.FindByIdAsync(identityUser.Id);

            if (user is null)
            {
                await _userManager.CreateAsync(identityUser);

                return await Task.FromResult(new Result<IdentityUser>()
                {
                    Value = identityUser,
                    Message = "User was added successfully",
                });
            }

            return await Task.FromResult(new Result<IdentityUser>()
            {
                ExceptionMessage = "User already exists"
            });
        }

        public async Task<Result<IdentityUser>> UpdateAsync(Guid id, IdentityUser identityUser)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user is not null)
            {
                user = identityUser;

                await _userManager.UpdateAsync(user);

                return await Task.FromResult(new Result<IdentityUser>()
                {
                    Value = user,
                    Message = "User was added successfully",
                });
            }

            return await Task.FromResult(new Result<IdentityUser>()
            {
                ExceptionMessage = "User doesn't exist"
            });
        }

        public async Task<Result<IdentityUser>> DeleteAsync(IdentityUser identityUser)
        {
            var user = await _userManager.FindByIdAsync(identityUser.Id);

            if (user is not null)
            {
                await _userManager.DeleteAsync(identityUser);

                return await Task.FromResult(new Result<IdentityUser>()
                {
                    Value = identityUser,
                    Message = "User was added successfully",
                });
            }

            return await Task.FromResult(new Result<IdentityUser>()
            {
                ExceptionMessage = "User doesn't exist"
            });
        }
    }
}
