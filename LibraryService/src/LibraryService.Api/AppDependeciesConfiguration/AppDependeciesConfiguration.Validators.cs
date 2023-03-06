using LibraryService.Api.Models;
using LibraryService.Api.Validators;
using LibraryService.BussinesLogic.Validators.Abstract;

namespace LibraryService.Api.AppDependeciesConfiguration
{
    public static partial class AppDependeciesConfiguration
    {
        public static WebApplicationBuilder AddValidators(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IValidator<AuthorRequestModel>, AuthorValidator>();
            builder.Services.AddScoped<IValidator<BookRequestModel>, BookValidator>();
            builder.Services.AddScoped<IValidator<GenreRequestModel>, GenreValidator>();
            builder.Services.AddScoped<IValidator<PublisherRequestModel>, PublisherValidator>();

            return builder;
        }
    }
}
