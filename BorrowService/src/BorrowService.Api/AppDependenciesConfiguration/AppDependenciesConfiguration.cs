namespace BorrowService.Api.AppDependenciesConfiguration
{
    public static partial class AppDependenciesConfiguration
    {
        public static WebApplicationBuilder ConfigureDependencies(this WebApplicationBuilder builder)
        {
            builder.AddServices()
                .AddHostedServices()
                .AddOptions()
                .AddExternalDependencies()
                .AddValidators();
            
            return builder;
        }
    }
}
