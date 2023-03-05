using Serilog;
using Serilog.Exceptions;

namespace BorrowService.Api.AppDependenciesConfiguration
{
    public static partial class AppDependenciesConfiguration
    {
        public static WebApplicationBuilder AddLogging(this WebApplicationBuilder builder)
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Async(options =>
                {
                    options.Console();
                })
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);

            return builder;
        }
    }
}
