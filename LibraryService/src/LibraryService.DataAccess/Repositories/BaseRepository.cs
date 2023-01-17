using LibrarySevice.DataAccess.Entities.Abstract;
using LibrarySevice.DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace LibrarySevice.DataAccess.Repositories
{
    public class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity, TContext>
        where TEntity : Base
        where TContext : DbContext
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

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
