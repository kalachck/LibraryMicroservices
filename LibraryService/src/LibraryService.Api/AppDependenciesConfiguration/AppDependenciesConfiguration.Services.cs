using FluentValidation;
using LibrarySevice.Api.Models;
using LibrarySevice.Api.Validators;
using LibrarySevice.BussinesLogic.BackgroundServices;
using LibrarySevice.BussinesLogic.Options;
using LibrarySevice.BussinesLogic.Services;
using LibrarySevice.BussinesLogic.Services.Abstract;
using LibrarySevice.DataAccess;
using LibrarySevice.DataAccess.Entities;
using LibrarySevice.DataAccess.Repositories;
using LibrarySevice.DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LibrarySevice.Api.AppDependeciesConfiguration
{
    public static partial class AppDependenciesConfiguration
    {
        public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("LibraryConnection"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            builder.Services.AddScoped<IBaseRepository<Author, ApplicationContext>, BaseRepository<Author, ApplicationContext>>();
            builder.Services.AddScoped<IBaseRepository<Book, ApplicationContext>, BaseRepository<Book, ApplicationContext>>();
            builder.Services.AddScoped<IBaseRepository<Genre, ApplicationContext>, BaseRepository<Genre, ApplicationContext>>();
            builder.Services.AddScoped<IBaseRepository<Publisher, ApplicationContext>, BaseRepository<Publisher, ApplicationContext>>();

            builder.Services.AddScoped<IAuthorService, AuthorService>();
            builder.Services.AddScoped<IBookService, BussinesLogic.Services.BookService>();
            builder.Services.AddScoped<IPublisherService, PublisherService>();
            builder.Services.AddScoped<IGenreService, GenreService>();

            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            builder.Services.AddScoped<IValidator<BookRequestModel>, BookValidator>();
            builder.Services.AddScoped<IValidator<AuthorRequestModel>, AuthorValidator>();
            builder.Services.AddScoped<IValidator<PublisherRequestModel>, PublisherValidator>();
            builder.Services.AddScoped<IValidator<GenreRequestModel>, GenreValidator>();

            builder.Services.Configure<RabbitOptions>(
                builder.Configuration.GetSection(RabbitOptions.RabbitData));

            builder.Services.AddHostedService<RabbitService>();

            builder.Services.AddGrpc();

            return builder;
        }
    }
}
