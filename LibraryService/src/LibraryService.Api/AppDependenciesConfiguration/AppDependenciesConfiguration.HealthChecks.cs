using LibraryService.Api.HealthChecks;

namespace LibraryService.Api.AppDependenciesConfiguration
{
    public static partial class AppDependenciesConfiguration
    {
        public static WebApplicationBuilder AddHealthChecks(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("LibraryConnection");

            builder.Services.AddHealthChecks()
                .AddCheck<DatabaseHealthCheck>("DbHealthCheck")
                .AddCheck<RabbitMqHealthCheck>("RabbitMqHealthCheck");

            builder.Services.AddHealthChecksUI(options =>
            {
                options.SetEvaluationTimeInSeconds(15);
                options.MaximumHistoryEntriesPerEndpoint(30);
            })
                .AddSqlServerStorage(connectionString);

            return builder;
        }
    }
}
