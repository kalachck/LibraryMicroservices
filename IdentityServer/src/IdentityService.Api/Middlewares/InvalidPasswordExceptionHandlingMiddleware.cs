using IdentityService.BusinessLogic.Exceptions;
using System.Net;

namespace IdentityService.Api.Middlewares
{
    public class InvalidPasswordExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public InvalidPasswordExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (InvalidPasswordException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsync(ex.Message);
            }
        }
    }
}
