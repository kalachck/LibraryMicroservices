namespace BorrowService.Borrowings.Services.Abstract
{
    public interface IMailService
    {
        Task SendMessageAsync(string email, string bookTitle);
    }
}
