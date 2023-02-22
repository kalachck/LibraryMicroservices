using BorrowService.Borrowings.Entities;

namespace BorrowService.Borrowings.Repositories.Abstract
{
    public interface IBorrowingRepository
    {
        Task<Borrowing> GetByEmailAsync(string email);

        Task<Borrowing> GetByEmailAndTitleAsync(string email, string title);

        Task<List<Borrowing>> GetBorrowingsAsync();

        void Add(Borrowing entity);

        void Update(Borrowing entity);

        void Delete(Borrowing entity);

        Task SaveChangesAsync();
    }
}
