using IdentityService.Api.HealthChecks;

namespace IdentityService.Api.AppDependenciesConfiguration
{
    public static partial class AppDependenciesConfiguration
    {
        public static WebApplicationBuilder AddHealthChecks(this WebApplicationBuilder builder)
        {
            builder.Services.AddHealthChecks()
                .AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                .AddCheck<MailServiceHealthCheck>("MailServiceHealthCheck");

            return builder;
        }
    }
}
