namespace LibraryService.Api.AppDependeciesConfiguration
{
    public static partial class AppDependeciesConfiguration
    {
        public static WebApplicationBuilder ConfigureDependencies(this WebApplicationBuilder builder)
        {
            builder.AddServices()
                .AddLogging();

            return builder;
        }
    }
}
