using IdentityService.Api.Models;
using IdentityService.Api.Validators;
using IdentityService.BusinessLogic.Validators.Abstract;

namespace IdentityService.Api.AppDependenciesConfiguration
{
    public static partial class AppDependenciesConfiguration
    {
        public static WebApplicationBuilder AddValidators(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IValidator<LoginModel>, LoginModelValidator>();

            return builder;
        }
    }
}
