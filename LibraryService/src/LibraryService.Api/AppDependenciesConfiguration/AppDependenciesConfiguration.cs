using LibrarySevice.Api.AppDependenciesConfiguration;

namespace LibrarySevice.Api.AppDependeciesConfiguration
{
    public static partial class AppDependenciesConfiguration
    {
        public static WebApplicationBuilder ConfigureDependencies(this WebApplicationBuilder builder)
        {
            builder.AddServices();

            builder.AddHealthChecks();

            return builder;
        }
    }
}
