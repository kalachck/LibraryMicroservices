using IdentityServer.DataAccess.Entities;

namespace IdentityServer.DataAccess.Repositories.Abstract
{
    public interface IUserRepository
    {
        Task<UserEntity> GetByIdAsync(int id);

        Task<UserEntity> GetAsync(string login, string password);

        Task UpdateAsync(UserEntity userEntity);

        Task DeleteAsync(UserEntity userEntity);
    }
}
