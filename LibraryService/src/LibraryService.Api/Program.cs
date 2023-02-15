using HealthChecks.UI.Client;
using LibrarySevice.Api.AppDependeciesConfiguration;
using LibrarySevice.BussinesLogic.Services;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace LibrarySevice.Api
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

            app.MapGrpcService<CheckBookService>();

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
