using LibrarySevice.DataAccess.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace LibrarySevice.DataAccess.Repositories.Abstract
{
    public interface IBaseRepository<TEntity, TContext> 
        where TEntity : Base
        where TContext: DbContext
    {
        TEntity GetAsync(int id);

        void UpdateAsync(TEntity entity);

        void AddAsync(TEntity entity);

        void DeleteAsync(TEntity entity);
    }
}
