using BorrowService.Hangfire.Options;
using BorrowService.Hangfire.Services.Abstract;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace BorrowService.Hangfire.Services
{
    public class MailService : IMailService
    {
        private readonly MailOptions _options;

        public MailService(IOptions<MailOptions> options)
        {
            _options = options.Value;
        }

        public async Task<MimeMessage> ConfigureMessage(string email, string bookTitle)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("LibraryService", _options.SourceMail));
            message.To.Add(new MailboxAddress("User", email));
            message.Subject = _options.Subject;

            message.Body = new TextPart(TextFormat.Html)
            {
                Text = $"The ownership of the book {bookTitle} is coming to an end, you can extend the ownership or return the book to the library."
            };

            return await Task.FromResult(message);
        }

        public async Task SendMessageAsync(string email, string bookTitle)
        {
            var message = await ConfigureMessage(email, bookTitle);

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.mail.ru", 465, true);
                await client.AuthenticateAsync(_options.SourceMail, _options.SourcePassword);

                await client.SendAsync(message);

                await client.DisconnectAsync(true);
            }
        }
    }
}
