using BorrowService.Borrowings.Entities;
using BorrowService.Borrowings.Repositories.Abstract;
using BorrowService.Borrowings.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace BorrowService.Borrowings.Repositories
{
    public class BorrowingRepository : IBorrowingRepository
    {
        private readonly DbSet<Borrowing> _borrowings;
        private readonly IDbSaver _dbSaver;

        public BorrowingRepository(ApplicationContext applicationContext,
            IDbSaver dbSaver)
        {
            _borrowings = applicationContext.Borrowings;
            _dbSaver = dbSaver;
        }

        public async Task<Borrowing> GetByEmailAsync(string email)
        {
            return await _borrowings.AsNoTracking().FirstOrDefaultAsync(x => x.UserEmail == email);
        }

        public async Task<Borrowing> GetByEmailAndTitleAsync(string email, string title)
        {
            return await _borrowings.AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserEmail == email && x.BookTitle == title);
        }

        public async Task<List<Borrowing>> GetBorrowingsAsync()
        {
            return await _borrowings.Where(x => x.ExpirationDate == DateTime.Now.Date + TimeSpan.FromDays(3)).ToListAsync();
        }

        public void Add(Borrowing entity)
        {
            _borrowings.Add(entity);
        }

        public void Update(Borrowing entity)
        {
            _borrowings.Update(entity);
        }

        public void Delete(Borrowing entity)
        {
            _borrowings.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _dbSaver.SaveChangesAsync();
        }
    }
}
