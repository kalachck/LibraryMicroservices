using AutoMapper;
using IdentityService.BusinessLogic.Exceptions;
using IdentityService.BusinessLogic.Services.Abstarct;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;

        public UserService(UserManager<IdentityUser> userManager,
            IMailService mailService,
            IMapper mapper)
        {
            _userManager = userManager;
            _mailService = mailService;
            _mapper = mapper;
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

            throw new AlreadyExistsException("This user already exists");
        }

        public async Task<IdentityUser> UpdateAsync(string email, IdentityUser identityUser)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var id = user.Id;

                user = _mapper.Map(identityUser, user);

                user.Id = id;

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

        public async Task<IdentityUser> UpdatePasswordAsync(string email, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                currentPassword = _userManager.PasswordHasher.HashPassword(user, currentPassword);

                if (await _userManager.CheckPasswordAsync(user, currentPassword))
                {
                    newPassword = _userManager.PasswordHasher.HashPassword(user, newPassword);

                    await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

                    return await Task.FromResult(user);
                }

                throw new InvalidPasswordException("Current password doesn't match with the typed one");
            }

            throw new NotFoundException("User with this email doesn't exists");
        }

        public async Task<string> ResetPasswordAsync(string email)
        {
            var resetCode = await GenerateResetCodeAsync();

            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                await _mailService.SendMessageAsync(email, resetCode, "Reset password");

                return await Task.FromResult(resetCode);
            }

            throw new NotFoundException("User with this email doesn't exists");
        }

        private async Task<string> GenerateResetCodeAsync()
        {
            var random = new Random();

            var resetCode = $"Your reset code is: {random.Next(100, 1000)}-{random.Next(100, 1000)}";

            return await Task.FromResult(resetCode);
        }
    }
}
