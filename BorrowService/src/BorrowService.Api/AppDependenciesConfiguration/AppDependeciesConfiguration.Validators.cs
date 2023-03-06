using BorrowService.Api.Models;
using BorrowService.Api.Validators;
using BorrowService.Borrowings.Validators.Abstract;

namespace BorrowService.Api.AppDependenciesConfiguration
{
    public static partial class AppDependeciesConfiguration
    {
        public static WebApplicationBuilder AddValidators(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IValidator<BorrowingRequestModel>, BorrowingValidator>();

            return builder;
        }
    }
}
