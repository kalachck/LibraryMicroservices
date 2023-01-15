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

        public TEntity GetAsync(int id)
        {
            return _dbSet.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public void AddAsync(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
