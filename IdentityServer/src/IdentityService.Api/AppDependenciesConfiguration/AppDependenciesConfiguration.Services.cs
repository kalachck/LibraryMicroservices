using IdentityService.BusinessLogic.Options;
using IdentityService.BusinessLogic.Services;
using IdentityService.BusinessLogic.Services.Abstarct;
using IdentityService.DataAccess;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
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
                    .RequireProofKeyForCodeExchange()
                    .AllowRefreshTokenFlow();

                    options.SetAuthorizationEndpointUris("/api/Authorization/LogIn");
                    options.SetLogoutEndpointUris("/api/Authorization/LogOut");
                    options.SetTokenEndpointUris("/api/Authorization/Token");

                    options
                    .AddEphemeralEncryptionKey()
                    .AddEphemeralSigningKey()
                    .DisableAccessTokenEncryption();

                    options.SetAccessTokenLifetime(TimeSpan.FromHours(1));
                    options.SetRefreshTokenLifetime(TimeSpan.FromDays(30));

                    options.RegisterScopes("api");

                    options.UseAspNetCore()
                    .DisableTransportSecurityRequirement()
                    .EnableTokenEndpointPassthrough()
                    .EnableAuthorizationEndpointPassthrough()
                    .EnableUserinfoEndpointPassthrough();
                });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
            {
                options.ClientId = builder.Configuration.GetValue<string>("GoogleAuth:ClientId");
                options.ClientSecret = builder.Configuration.GetValue<string>("GoogleAuth:ClientSecret");
                options.SignInScheme = IdentityConstants.ExternalScheme;
            });

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
            builder.Services.AddScoped<IMailService, MailService>();

            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            builder.Services.Configure<MailOptions>(
                builder.Configuration.GetSection(MailOptions.MailData));

            builder.Services.AddGrpc();

            return builder;
        }
    }
}
