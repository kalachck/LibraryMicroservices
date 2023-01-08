using LibrarySevice.DataAccess.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace LibrarySevice.DataAccess.Repositories.Abstract
{
    public abstract class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity> 
        where TEntity : BaseEntity
        where TContext: DbContext
    {
        private readonly TContext _context;

        public BaseRepository(TContext context)
        {
            _context = context;
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<TEntity>> TakeAsync(int amount)
        {
            return await _context.Set<TEntity>().AsNoTracking().Take(amount).ToListAsync();
        }

        public async Task<TEntity> UpsertAsync(TEntity entity)
        {
            if (_context.Set<TEntity>().Any(x => x.Id == entity.Id))
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                _context.Entry(entity).State = EntityState.Added;
            }

            await _context.SaveChangesAsync();

            return await Task.FromResult(entity);
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;

            await _context.SaveChangesAsync();

            return await Task.FromResult(entity);
        }
    }
}
