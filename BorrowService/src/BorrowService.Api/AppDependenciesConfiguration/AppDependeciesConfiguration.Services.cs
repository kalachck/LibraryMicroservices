using BorrowService.Borrowings;
using BorrowService.Borrowings.Components;
using BorrowService.Borrowings.Components.Abstract;
using BorrowService.Borrowings.Options;
using BorrowService.Borrowings.Repositories;
using BorrowService.Borrowings.Repositories.Abstract;
using BorrowService.Borrowings.Services;
using BorrowService.Borrowings.Services.Abstract;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;

namespace BorrowService.Api.AppDependenciesConfiguration
{
    public static partial class AppDependeciesConfiguration
    {
        public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("BorrowConnection");

            builder.Services.AddHttpClient();

            builder.Services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            builder.Services.AddHangfire(options =>
            {
                options.UsePostgreSqlStorage(connectionString);
            });

            builder.Services.AddHangfireServer();

            builder.Services.AddScoped<IBorrowingRepository, BorrowingRepository>();

            builder.Services.AddScoped<IBorrowingComponent, BorrowingComponent>();

            builder.Services.AddScoped<IMailService, MailService>();

            builder.Services.AddScoped<IHangfireService, HangfireService>();

            builder.Services.Configure<CommunicationOptions>(
                builder.Configuration.GetSection(CommunicationOptions.CommunicationUrls));

            builder.Services.Configure<MailOptions>(
                builder.Configuration.GetSection(MailOptions.MailData));

            return builder;
        }
    }
}
