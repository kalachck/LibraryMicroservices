using BorrowService.Borrowings.Entities;

namespace BorrowService.Borrowings.Components.Abstract
{
    public interface IBorrowingComponent
    {
        Task<Borrowing> GetAsync(string email, string title);

        Task<bool> BorrowAsync(string email, string title, int borrowingPeriod);

        Task<bool> ExtendAsync(string email, string title, int borrowingPeriod);

        Task<bool> DeleteAsync(string email, string title);

        Task<bool> CheckUserAsync(string email);

        Task<Dictionary<string, string>> GetBookAsync(string email);

        Task<bool> CheckBookAsync(int bookId);
    }
}
