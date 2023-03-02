using FluentValidation;
using LibraryService.Api.Models;
using LibraryService.Api.Validators;
using LibraryService.BussinesLogic.Services;
using LibraryService.BussinesLogic.Services.Abstract;
using LibraryService.DataAccess;
using LibraryService.DataAccess.Entities;
using LibraryService.DataAccess.Repositories;
using LibraryService.DataAccess.Repositories.Abstract;
using LibraryService.RabbitMq.Options;
using LibraryService.RabbitMq.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LibraryService.Api.AppDependeciesConfiguration
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
            builder.Services.AddScoped<IBaseRepository<Genre, ApplicationContext>, BaseRepository<Genre, ApplicationContext>>();
            builder.Services.AddScoped<IBaseRepository<Publisher, ApplicationContext>, BaseRepository<Publisher, ApplicationContext>>();
            builder.Services.AddScoped<IBookRepository, BookRepository>();

            builder.Services.AddScoped<IAuthorService, AuthorService>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IPublisherService, PublisherService>();
            builder.Services.AddScoped<IGenreService, GenreService>();

            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            builder.Services.AddScoped<IValidator<BookRequestModel>, BookValidator>();
            builder.Services.AddScoped<IValidator<AuthorRequestModel>, AuthorValidator>();
            builder.Services.AddScoped<IValidator<PublisherRequestModel>, PublisherValidator>();
            builder.Services.AddScoped<IValidator<GenreRequestModel>, GenreValidator>();

            builder.Services.AddScoped<IDbManager<Book>, DbManager<Book>>();
            builder.Services.AddScoped<IDbManager<Author>, DbManager<Author>>();
            builder.Services.AddScoped<IDbManager<Genre>, DbManager<Genre>>();
            builder.Services.AddScoped<IDbManager<Publisher>, DbManager<Publisher>>();

            builder.Services.Configure<RabbitOptions>(
                builder.Configuration.GetSection(RabbitOptions.RabbitData));

            builder.Services.AddHostedService<RabbitService>();

            builder.Services.AddGrpc();

            return builder;
        }
    }
}
