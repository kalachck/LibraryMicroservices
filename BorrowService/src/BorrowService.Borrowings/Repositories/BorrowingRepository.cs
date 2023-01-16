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

        public async Task<Borrowing> GetAsync(int id)
        {
            return await _applicationContext.Borrowings.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Borrowing> GetByBookIdAsync(int bookId)
        {
            return await _applicationContext.Borrowings.AsNoTracking().FirstOrDefaultAsync(x => x.BookId == bookId);
        }

        public async Task<Borrowing> GetByEmailAsync(string email)
        {
            return await _applicationContext.Borrowings.AsNoTracking().FirstOrDefaultAsync(x => x.UserEmail == email);
        }

        public void Add(Borrowing entity)
        {
            _applicationContext.Borrowings.Add(entity);
        }

        public void Update(Borrowing entity)
        {
            _applicationContext.Borrowings.Update(entity);
        }

        public void Delete(Borrowing entity)
        {
            _applicationContext.Borrowings.Remove(entity);
        }
    }
}
