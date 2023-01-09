using IdentityService.BusinessLogic.Options;
using IdentityService.BusinessLogic.Services.Abstarct;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace IdentityService.BusinessLogic.Services
{
    public class MailService : IMailService
    {
        private readonly MailOptions _mailOptions;

        public MailService(IOptions<MailOptions> mailOptions)
        {
            _mailOptions = mailOptions.Value;
        }

        public async Task SendMessageAsync(string email, string resetCode, string subject)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("IdentityService", _mailOptions.SourceMail));
            message.To.Add(new MailboxAddress("IdentityUser", email));
            message.Subject = subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = resetCode,
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
