using BorrowService.Borrowings.Entities;

namespace BorrowService.Borrowings.Components.Abstract
{
    public interface IBorrowingComponent
    {
        Task<Borrowing> GetAsync(int id);

        Task<Borrowing> GetByBookIdAsync(int id);

        Task<Borrowing> GetByEmailAsync(string email);

        Task<string> BorrowAsync(string email, int bookId, HttpClient httpClient);

        Task<string> UpdateAsync(int id, Borrowing entity);

        Task<string> DeleteAsync(int id);
    }
}
