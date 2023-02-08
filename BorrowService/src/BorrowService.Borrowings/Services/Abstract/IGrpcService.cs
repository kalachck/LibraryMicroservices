using BorrwoService.Borrowing;

namespace BorrowService.Borrowings.Services.Abstract
{
    public interface IGrpcService
    {
        Task<bool> CheckUser(string email);

        Task<BookResponse> CheckBook(int bookId);
    }
}
