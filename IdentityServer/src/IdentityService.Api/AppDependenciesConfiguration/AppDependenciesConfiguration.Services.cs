using IdentityService.BusinessLogic.Providers;
using IdentityService.BusinessLogic.Providers.Abstract;
using IdentityService.BusinessLogic.Services;
using IdentityService.BusinessLogic.Services.Abstarct;
using IdentityService.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IdentityService.Api.AppDependenciesConfiguration
{
    public static partial class AppDependenciesConfiguration
    {
        public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
        {
            var configuration = builder.Configuration;

            builder.Services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

                options.UseOpenIddict();
            });

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();

            builder.Services.AddOpenIddict()
                .AddServer(options =>
                {
                    options.SetAuthorizationEndpointUris("api/Authorization/LogIn");

                    options.AllowAuthorizationCodeFlow();

                    options.UseAspNetCore()
                    .EnableTokenEndpointPassthrough();
                });

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAuthorizationProvider, AuthorizationProvider>();

            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return builder;
        }
    }
}
