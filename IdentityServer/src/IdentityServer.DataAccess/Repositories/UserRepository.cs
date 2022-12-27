using IdentityServer.DataAccess.Entities;
using IdentityServer.DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext applicationContext;

        public UserRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public async Task<UserEntity> GetByIdAsync(int id)
        {
            return await applicationContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UserEntity> GetAsync(string login, string password)
        {
            return await applicationContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserName == login && x.PasswordHash == password);
        }

        public async Task UpdateAsync(UserEntity userEntity)
        {
            if (!await applicationContext.Users.AnyAsync(x => x.Id == userEntity.Id))
            {
                applicationContext.Entry(userEntity).State = EntityState.Added;
            }
            else
            {
                applicationContext.Entry(userEntity).State = EntityState.Modified;
            }

            await applicationContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(UserEntity userEntity)
        {
            applicationContext.Entry(userEntity).State = EntityState.Deleted;

            await applicationContext.SaveChangesAsync();
        }
    }
}
