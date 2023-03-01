using LibraryService.RabbitMq.Options;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace LibraryService.Api.HealthChecks
{
    public class RabbitMqHealthCheck : IHealthCheck
    {
        private readonly RabbitOptions _options;

        public RabbitMqHealthCheck(IOptions<RabbitOptions> options)
        {
            _options = options.Value;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = _options.HostName,
                    Port = _options.Port,
                    UserName = _options.UserName,
                    Password = _options.Password,
                };

                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();
            }
            catch (Exception ex)
            {
                return new HealthCheckResult(context.Registration.FailureStatus, ex.Message);
            }

            return HealthCheckResult.Healthy();
        }
    }
}
