using BorrowService.Borrowings.Services.Abstract;

namespace BorrowService.Borrowings.Services
{
    public class DbSaver : IDbSaver
    {
        private readonly ApplicationContext _applicationContext;

        public DbSaver(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task SaveChangesAsync()
        {
            await _applicationContext.SaveChangesAsync();
        }
    }
}
