using BorrowService.Api.AppDependenciesConfiguration;
using BorrowService.Api.Middlewares;
using Hangfire;
using Microsoft.AspNetCore.Mvc.Filters;

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

            var options = new DashboardOptions()
            {
                Authorization = new[] { new AuthorizationFilter() }
            };

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

            app.UseHangfireDashboard("/dashboard", options);

            app.MapControllers();

            app.Run();
        }
    }
}
