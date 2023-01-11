using BorrowService.Borrowings.Entities;
using BorrowService.Borrowings.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace BorrowService.Borrowings.Repositories
{
    public class BorrowingRepository : IBorrowingRepository
    {
        private readonly ApplicationContext _applicationContext;

        public BorrowingRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<List<BorrowingEntity>> TakeAsync(int amount)
        {
            return await _applicationContext.Borrowings.AsNoTracking().Take(amount).ToListAsync();
        }

        public async Task<BorrowingEntity> GetAsync(int id)
        {
            return await _applicationContext.Borrowings.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BorrowingEntity> GetByBookIdAsync(int bookId)
        {
            return await _applicationContext.Borrowings.AsNoTracking().FirstOrDefaultAsync(x => x.BookId == bookId);
        }

        public async Task<BorrowingEntity> GetByEmailAsync(string email)
        {
            return await _applicationContext.Borrowings.AsNoTracking().FirstOrDefaultAsync(x => x.UserEmail == email);
        }

        public async Task<BorrowingEntity> AddAsync(BorrowingEntity entity)
        {
            _applicationContext.Borrowings.Add(entity);

            return await Task.FromResult(entity);
        }

        public async Task<BorrowingEntity> UpdateAsync(BorrowingEntity entity)
        {
            _applicationContext.Borrowings.Update(entity);

            return await Task.FromResult(entity);
        }

        public async Task<BorrowingEntity> DeleteAsync(BorrowingEntity entity)
        {
            _applicationContext.Entry(entity).State = EntityState.Deleted;

            await _applicationContext.SaveChangesAsync();

            return await Task.FromResult(entity);
        }
    }
}
