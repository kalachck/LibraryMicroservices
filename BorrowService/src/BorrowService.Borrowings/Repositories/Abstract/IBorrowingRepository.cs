using BorrowService.Borrowings.Entities;

namespace BorrowService.Borrowings.Repositories.Abstract
{
    public interface IBorrowingRepository
    {
        Task<List<BorrowingEntity>> TakeAsync(int amount);

        Task<BorrowingEntity> GetAsync(int id);

        Task<BorrowingEntity> GetByBookIdAsync(int id);

        Task<BorrowingEntity> GetByEmailAsync(string email);

        Task<BorrowingEntity> AddAsync(BorrowingEntity entity);

        Task<BorrowingEntity> UpdateAsync(BorrowingEntity entity);

        Task<BorrowingEntity> DeleteAsync(BorrowingEntity entity);
    }
}
