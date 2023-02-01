using LibrarySevice.DataAccess.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace LibrarySevice.DataAccess.Repositories.Abstract
{
    public interface IBaseRepository<TEntity, TContext> 
        where TEntity : Base
        where TContext: DbContext
    {
        Task<TEntity> GetAsync(int id);

        void Update(TEntity entity);

        void Add(TEntity entity);

        void Delete(TEntity entity);
    }
}
