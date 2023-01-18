using BorrowService.Borrowings.Options;
using BorrowService.Borrowings.Services.Abstract;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace BorrowService.Borrowings.Services
{
    public class MailService : IMailService
    {
        private readonly MailOptions _mailOptions;

        public MailService(IOptions<MailOptions> options)
        {
            _mailOptions = options.Value;
        }

        public async Task SendMessageAsync(string email, string bookTitle)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("LibraryService", _mailOptions.SourceMail));
            message.To.Add(new MailboxAddress("IdentityUser", email));
            message.Subject = _mailOptions.Subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $"The ownership of the book {bookTitle} is coming to an end, you can extend the ownership or wait for the expiration date." +
                $" The book will be automatically deleted from your library",
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.mail.ru", 465, true);
                await client.AuthenticateAsync(_mailOptions.SourceMail, _mailOptions.SourcePassword);
                await client.SendAsync(message);

                await client.DisconnectAsync(true);
            }
        }
    }
}
