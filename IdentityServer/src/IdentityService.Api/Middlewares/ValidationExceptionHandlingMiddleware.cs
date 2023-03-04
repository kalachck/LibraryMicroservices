using System.ComponentModel.DataAnnotations;
using System.Net;

namespace IdentityService.Api.Middlewares
{
    public class ValidationExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsync(ex.Message);
            }
        }
    }
}
