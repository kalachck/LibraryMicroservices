using BorrowService.Api.Models;
using BorrowService.Api.Validators;
using BorrowService.Borrowings.Components;
using BorrowService.Borrowings.Components.Abstract;
using BorrowService.Borrowings.Repositories;
using BorrowService.Borrowings.Repositories.Abstract;
using BorrowService.Borrowings.Services;
using BorrowService.Borrowings.Services.Abstract;
using BorrowService.Hangfire.Services;
using BorrowService.Hangfire.Services.Abstract;
using BorrowService.RabbitMq.Services;
using BorrowService.RabbitMq.Services.Abstract;
using FluentValidation;

namespace BorrowService.Api.AppDependenciesConfiguration
{
    public static partial class AppDependeciesConfiguration
    {
        public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpClient();

            builder.Services.AddScoped<IBorrowingRepository, BorrowingRepository>();
            builder.Services.AddScoped<IBorrowingComponent, BorrowingComponent>();

            builder.Services.AddScoped<IDbSaver, DbSaver>();

            builder.Services.AddScoped<IValidator<BorrowingRequestModel>, BorrowingValidator>();

            builder.Services.AddScoped<IMailService, MailService>();

            builder.Services.AddScoped<IRabbitService, RabbitService>();
            
            return builder;
        }
    }
}
