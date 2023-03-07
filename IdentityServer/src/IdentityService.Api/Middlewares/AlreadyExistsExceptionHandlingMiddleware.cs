using IdentityService.BusinessLogic.Exceptions;
using System.Net;

namespace IdentityService.Api.Middlewares
{
    public class AlreadyExistsExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public AlreadyExistsExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AlreadyExistsException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsync(ex.Message);
            }
        }
    }
}
