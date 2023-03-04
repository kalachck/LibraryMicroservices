using LibraryService.Api.AppDependeciesConfiguration;
using LibraryService.Api.Middlewares;
using LibraryService.BussinesLogic.Services;

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

            app.UseMiddleware<ValidationExceptionHandlingMiddleware>();

            app.UseAuthorization();

            app.MapGrpcService<GrpcCheckBookService>();
            app.MapGrpcService<GrpcGetBookService>();

            app.MapControllers();

            app.Run();
        }
    }
}
