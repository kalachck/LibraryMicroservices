using BorrowService.Api.HealthChecks;

namespace BorrowService.Api.AppDependenciesConfiguration
{
    public static partial class AppDependenciesConfiguration
    {
        public static WebApplicationBuilder AddHealthChecks(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("BorrowConnection");

            builder.Services.AddHealthChecks()
                .AddCheck<MailServiceHealthCheck>("MailHealthCheck")
                .AddCheck<RabbitMqHealthCheck>("RabbitMqHealthCheck")
                .AddNpgSql(connectionString)
                .AddHangfire(null);

            builder.Services.AddHealthChecksUI(options =>
            {
                options.SetEvaluationTimeInSeconds(15);
                options.MaximumHistoryEntriesPerEndpoint(30);
            })
                .AddPostgreSqlStorage(connectionString);

            return builder;
        }
    }
}
