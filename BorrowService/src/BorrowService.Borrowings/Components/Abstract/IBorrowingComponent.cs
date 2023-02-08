using BorrowService.Borrowings.Entities;

namespace BorrowService.Borrowings.Components.Abstract
{
    public interface IBorrowingComponent
    {
        Task<Borrowing> GetAsync(int id);

        Task<Borrowing> GetByBookIdAsync(int bookId);

        Task<Borrowing> GetByEmailAsync(string email);

        Task<Borrowing> GetByEmailAndBookIdAsync(string email, int bookId);

        Task<string> BorrowAsync(string email, int bookId);

        Task<string> ExtendAsync(string email, int bookId);

        Task<string> DeleteByEmailAndBookIdAsync(string email, int bookId);

        Task<string> DeleteAsync(int id);
    }
}
