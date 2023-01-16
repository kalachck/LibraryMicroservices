using BorrowService.Api.RequestModels;
using BorrowService.Api.Validators;
using BorrowService.Borrowings;
using BorrowService.Borrowings.Components;
using BorrowService.Borrowings.Components.Abstract;
using BorrowService.Borrowings.Options;
using BorrowService.Borrowings.Repositories;
using BorrowService.Borrowings.Repositories.Abstract;
using FluentValidation;
using System.Reflection;

namespace BorrowService.Api.AppDependenciesConfiguration
{
    public static partial class AppDependeciesConfiguration
    {
        public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpClient();

            builder.Services.AddDbContext<ApplicationContext>();

            builder.Services.AddScoped<IBorrowingRepository, BorrowingRepository>();

            builder.Services.AddScoped<IBorrowingComponent, BorrowingComponent>();

            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            builder.Services.AddScoped<IValidator<BorrowingRequestModel>, BorrowingModelValidator>();

            builder.Services.Configure<CommunicationOptions>(
                builder.Configuration.GetSection(CommunicationOptions.CommunicationUrls));

            return builder;
        }
    }
}
