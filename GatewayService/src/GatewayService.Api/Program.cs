using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace GatewayService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("ocelot.json", false, true)
                .AddEnvironmentVariables();

            builder.Services.AddOcelot();

            var app = builder.Build();

            app.UseHttpsRedirection();

            app.UseOcelot();

            app.Run();
        }
    }
}
