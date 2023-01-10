using LibrarySevice.DataAccess.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace LibrarySevice.DataAccess.Repositories.Abstract
{
    public abstract class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity> 
        where TEntity : BaseEntity
        where TContext: DbContext
    {
        private DbSet<TEntity> _dbSet;

        public BaseRepository(TContext context)
        {
            _dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<TEntity>> TakeAsync(int amount)
        {
            return await _dbSet.AsNoTracking().Take(amount).ToListAsync();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            _dbSet.Add(entity);

            return await Task.FromResult(entity);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);

            return await Task.FromResult(entity);
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);

            return await Task.FromResult(entity);
        }
    }
}
