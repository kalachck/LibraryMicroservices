using BorrowService.Borrowings;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;

namespace BorrowService.Api.AppDependenciesConfiguration
{
    public static partial class AppDependeciesConfiguration
    {
        public static WebApplicationBuilder AddExternalDependencies(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("Trusted");

            builder.Services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            builder.Services.AddHangfire(options =>
            {
                options.UsePostgreSqlStorage(connectionString);
            });

            builder.Services.AddHangfireServer();

            return builder;
        }
    }
}
