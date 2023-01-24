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

            builder.Services.AddHttpClient();

            builder.Services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("BorrowConnection"));
            });

            builder.Services.AddHangfire(options =>
            {
                options.UsePostgreSqlStorage(builder.Configuration.GetConnectionString("BorrowConnection"));
            });

            builder.Services.AddHangfireServer();

            builder.Services.AddScoped<IBorrowingRepository, BorrowingRepository>();

            builder.Services.AddScoped<IBorrowingComponent, BorrowingComponent>();

            builder.Services.AddScoped<IMailService, MailService>();

            builder.Services.AddScoped<IHangfireService, HangfireService>();

            builder.Services.AddScoped<IRabbitService, RabbitService>();

            builder.Services.Configure<CommunicationOptions>(
                builder.Configuration.GetSection(CommunicationOptions.CommunicationUrls));

            builder.Services.Configure<MailOptions>(
                builder.Configuration.GetSection(MailOptions.MailData));

            builder.Services.Configure<RabbitOptions>(
                builder.Configuration.GetSection(RabbitOptions.RabbitData));

            return builder;
        }
    }
}
