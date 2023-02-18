using LibrarySevice.Api.AppDependeciesConfiguration;
using LibrarySevice.BussinesLogic.Hubs;

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

            app.MapHub<NotificationHub>("/notifications");

            app.MapControllers();

            app.Run();
        }
    }
}
