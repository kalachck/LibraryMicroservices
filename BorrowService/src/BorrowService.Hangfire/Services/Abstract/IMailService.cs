using MimeKit;

namespace BorrowService.Hangfire.Services.Abstract
{
    public interface IMailService
    {
        Task SendMessageAsync(string email, string bookTitle);

        Task<MimeMessage> ConfigureMessage(string email, string bookTitle);
    }
}
