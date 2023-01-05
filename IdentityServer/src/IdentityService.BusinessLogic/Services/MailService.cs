using IdentityService.BusinessLogic.Services.Abstarct;
using MailKit.Net.Smtp;
using MimeKit;

namespace IdentityService.BusinessLogic.Services
{
    public class MailService : IMailService
    {
        private const string SourceMail = "identityservicebot@mail.ru";
        private const string SourcePassword = "asgvC4ichhKhELZs8Mg6";

        public async Task SendMessageAsync(string email)
        {
            var random = new Random();

            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("IdentityService", SourceMail));
            message.To.Add(new MailboxAddress("IdentityUser", email));
            message.Subject = "Reset password";
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $"Your reset code is: {random.Next(100, 1000)}-{random.Next(100, 1000)}",
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.mail.ru", 465, true);
                await client.AuthenticateAsync(SourceMail, SourcePassword);
                await client.SendAsync(message);

                await client.DisconnectAsync(true);
            }
        }
    }
}
