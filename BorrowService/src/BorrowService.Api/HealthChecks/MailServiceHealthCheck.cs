using BorrowService.Borrowings.Options;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace BorrowService.Api.HealthChecks
{
    public class MailServiceHealthCheck : IHealthCheck
    {
        private readonly MailOptions _mailOptions;

        public MailServiceHealthCheck(IOptions<MailOptions> mailOptions)
        {
            _mailOptions = mailOptions.Value;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.mail.ru", 465, true);
                    await client.AuthenticateAsync(_mailOptions.SourceMail, _mailOptions.SourcePassword);

                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                return new HealthCheckResult(context.Registration.FailureStatus, ex.Message);
            }

            return HealthCheckResult.Healthy();
        }
    }
}
