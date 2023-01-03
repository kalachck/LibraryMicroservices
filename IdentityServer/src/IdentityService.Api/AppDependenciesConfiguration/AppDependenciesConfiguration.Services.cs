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
                .AddCore(options =>
                {
                    options.UseEntityFrameworkCore()
                    .UseDbContext<ApplicationContext>();
                })
                .AddServer(options =>
                {
                    options.AllowClientCredentialsFlow()
                    .AllowAuthorizationCodeFlow()
                    .RequireProofKeyForCodeExchange();

                    options.SetAuthorizationEndpointUris("/api/Authorization/LogIn");
                    options.SetLogoutEndpointUris("/api/Authorization/LogOut");
                    options.SetTokenEndpointUris("/api/Authorization/LogIn/token");

                    options
                        .AddEphemeralEncryptionKey()
                        .AddEphemeralSigningKey()
                        .DisableAccessTokenEncryption();

                    options.RegisterScopes("api");

                    options.UseAspNetCore()
                    .EnableTokenEndpointPassthrough()
                    .EnableAuthorizationEndpointPassthrough();             
                });

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ILogInService, LogInService>();

            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return builder;
        }
    }
}
