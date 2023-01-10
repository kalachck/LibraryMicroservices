using LibrarySevice.DataAccess.Entities.Abstract;

namespace LibrarySevice.DataAccess.Repositories.Abstract
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<List<TEntity>> TakeAsync(int amount);

        Task<TEntity> GetAsync(int id);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> DeleteAsync(TEntity entity);
    }
}
