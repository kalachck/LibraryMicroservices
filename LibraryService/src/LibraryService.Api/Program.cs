using HealthChecks.UI.Client;
using LibraryService.Api.AppDependeciesConfiguration;
using LibraryService.BussinesLogic.Services;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
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

            app.UseMiddleware<ExceptionHandlingMiddlware>();
            app.UseMiddleware<NotFoundExceptionHandlingMiddleware>();

            app.MapGrpcService<GrpcCheckBookService>();
            app.MapGrpcService<GrpcGetBookService>();
            app.UseAuthorization();

            app.MapControllers();

            app.UseHealthChecks("/hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecksUI(config => config.UIPath = "/hc-ui");

            app.Run();
        }
    }
}
