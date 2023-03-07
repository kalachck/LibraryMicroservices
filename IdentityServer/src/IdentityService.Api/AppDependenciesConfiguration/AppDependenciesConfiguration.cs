namespace IdentityService.Api.AppDependenciesConfiguration
{
    public static partial class AppDependenciesConfiguration
    {
        public static WebApplicationBuilder ConfigureDependencies(this WebApplicationBuilder builder)
        {
            builder.AddServices()
                .AddLogging();

            return builder;
        }
    }
}
