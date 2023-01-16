using BorrowService.Borrowings.Entities;

namespace BorrowService.Borrowings.Repositories.Abstract
{
    public interface IBorrowingRepository
    {
        Task<Borrowing> GetAsync(int id);

        Task<Borrowing> GetByBookIdAsync(int id);

        Task<Borrowing> GetByEmailAsync(string email);

        void Add(Borrowing entity);

        void Update(Borrowing entity);

        void Delete(Borrowing entity);
    }
}
