using BorrowService.Api.Models;
using BorrowService.Api.Validators;
using BorrowService.Borrowings;
using BorrowService.Borrowings.Components;
using BorrowService.Borrowings.Components.Abstract;
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
            builder.Services.AddDbContext<ApplicationContext>();

            builder.Services.AddScoped<IBorrowingRepository, BorrowingRepository>();

            builder.Services.AddScoped<IBorrowingComponent, BorrowingComponent>();

            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            builder.Services.AddScoped<IValidator<BorrowingModel>, BorrowingModelValidator>();

            return builder;
        }
    }
}
