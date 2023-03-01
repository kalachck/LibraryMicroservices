namespace LibraryService.BussinesLogic.Services.Abstract
{
    public interface IDbManager<TEntity>
    {
        void DetacheEntity(TEntity entity);

        Task SaveChangesAsync();
    }
}
