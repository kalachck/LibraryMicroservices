using Serilog;
using Serilog.Exceptions;

namespace LibraryService.Api.AppDependeciesConfiguration
{
    public static partial class AppDependeciesConfiguration
    {
        public static WebApplicationBuilder AddLogging(this WebApplicationBuilder builder)
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Async(options =>
                {
                    options.Console();
                })
                .MinimumLevel.Information()
                .Enrich.WithExceptionDetails()
                .Enrich.FromLogContext()
                .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);

            return builder;
        }
    }
}
