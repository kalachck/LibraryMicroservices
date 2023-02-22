using BorrowService.Api.AppDependenciesConfiguration;
using BorrowService.Api.Middlewares;
using Hangfire;

namespace BorrowService.Api
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

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseMiddleware<NotFoundExceptionHandlingMiddleware>();
            app.UseMiddleware<AlreadyExistsExceptionHandlingMiddleware>();


            app.UseAuthorization();

            app.UseHangfireDashboard("/dashboard");

            app.MapControllers();

            app.Run();
        }
    }
}
