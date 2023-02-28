using BorrowService.Hangfire.Services;

namespace BorrowService.Api.AppDependenciesConfiguration
{
    public static partial class AppDependeciesConfiguration
    {
        public static WebApplicationBuilder AddHostedServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddHostedService<HangfireService>();

            return builder;
        }
    }
}
