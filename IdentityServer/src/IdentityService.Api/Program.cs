using IdentityService.Api.AppDependenciesConfiguration;
using IdentityService.Api.Middlewares;

namespace IdentityService.Api
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

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseMiddleware<NotFoundExceptionHandlingMiddleware>();
            app.UseMiddleware<AlreadyExistsExceptionHandlingMiddleware>();
            app.UseMiddleware<InvalidPasswordExceptionHandlingMiddleware>();

            app.UseAuthentication();

            app.UseAuthorization();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection(); 

            app.MapControllers();

            app.Run();
        }
    }
}
