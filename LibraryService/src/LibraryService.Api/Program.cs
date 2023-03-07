using LibraryService.Api.AppDependeciesConfiguration;
using LibraryService.Api.Middlewares;
using LibraryService.BussinesLogic.Services;
using LibraryService.Api.Middlewares;

namespace LibraryService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.ConfigureDependencies();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<ExceptionHandlingMiddlware>();
            app.UseMiddleware<ValidationExceptionHandlingMiddleware>();
            app.UseMiddleware<NotFoundExceptionHandlingMiddleware>();

            app.MapGrpcService<GrpcCheckBookService>();
            app.MapGrpcService<GrpcGetBookService>();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
