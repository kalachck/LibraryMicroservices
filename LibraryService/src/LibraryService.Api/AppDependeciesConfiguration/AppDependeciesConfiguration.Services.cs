using LibrarySevice.Api.Models;
using LibrarySevice.Api.Validators;
using LibrarySevice.BussinesLogic.Services;
using LibrarySevice.DataAccess;
using LibrarySevice.DataAccess.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using LibrarySevice.BussinesLogic.Services.Abstract;

namespace LibrarySevice.Api.AppDependeciesConfiguration
{
    public static partial class AppDependeciesConfiguration
    {
        public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<AuthorRepository>();
            builder.Services.AddScoped<BookRepository>();
            builder.Services.AddScoped<PublisherRepository>();
            builder.Services.AddScoped<GenreRepository>();

            builder.Services.AddScoped<IAuthorService, AuthorService>();
            builder.Services.AddScoped<IBookService, BussinesLogic.Services.BookService>();
            builder.Services.AddScoped<IPublisherService, PublisherService>();
            builder.Services.AddScoped<IGenreService, GenreService>();

            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            builder.Services.AddScoped<IValidator<BookRequestModel>, BookValidator>();
            builder.Services.AddScoped<IValidator<AuthorRequestModel>, AuthorValidator>();
            builder.Services.AddScoped<IValidator<PublisherRequestModel>, PublisherValidator>();
            builder.Services.AddScoped<IValidator<GenreRequestModel>, GenreValidator>();

            return builder;
        }
    }
}
