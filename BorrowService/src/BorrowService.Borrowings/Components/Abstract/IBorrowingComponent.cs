using BorrowService.Borrowings.Entities;

namespace BorrowService.Borrowings.Components.Abstract
{
    public interface IBorrowingComponent
    {
        Task<List<BorrowingEntity>> TakeAsync(int amount);

        Task<BorrowingEntity> GetAsync(int id);

        Task<BorrowingEntity> GetByBookIdAsync(int id);

        Task<BorrowingEntity> GetByEmailAsync(string email);

        Task<BorrowingEntity> AddAsync(BorrowingEntity entity);

        Task<BorrowingEntity> UpdateAsync(int id, BorrowingEntity entity);

        Task<BorrowingEntity> DeleteAsync(int id);
    }
}
