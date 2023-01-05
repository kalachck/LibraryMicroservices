using IdentityService.BusinessLogic.Services.Abstarct;
using MailKit.Net.Smtp;
using MimeKit;

namespace IdentityService.BusinessLogic.Services
{
    public class MailService : IMailService
    {
        private const string SourceMail = "identityserverbot@gmail.com";
        private const string SourcePassword = "supersecretkey";

        public async Task SendMessageAsync(string email)
        {
            var random = new Random();

            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("IdentityService", SourceMail));
            message.To.Add(new MailboxAddress("IdentityUser", "kalachck@gmail.com"));
            message.Subject = "Reset password";
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $"Your reset code is: {random.Next(100, 1000)}-{random.Next(100, 1000)}",
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, true);
                await client.AuthenticateAsync(SourceMail, SourcePassword);
                await client.SendAsync(message);

                await client.DisconnectAsync(true);
            }
        }
    }
}
