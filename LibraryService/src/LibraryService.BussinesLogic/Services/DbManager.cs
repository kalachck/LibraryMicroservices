using LibraryService.BussinesLogic.Services.Abstract;
using LibraryService.DataAccess;
using LibraryService.DataAccess.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.BussinesLogic.Services
{
    public class DbManager<TEntity> : IDbManager<TEntity>
        where TEntity : Base
    {
        private readonly ApplicationContext _applicationContext;

        public DbManager(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public void DetacheEntity(TEntity entity)
        {
            _applicationContext.Entry(entity).State = EntityState.Detached;
        }

        public async Task SaveChangesAsync()
        {
            await _applicationContext.SaveChangesAsync();
        }
    }
}
