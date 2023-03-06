using LibraryService.BussinesLogic.Exceptions;
using System.Net;

namespace LibraryService.Api.Middlewares
{
    public class NotFoundExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public NotFoundExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                context.Response.WriteAsync(ex.Message);
            }
        }
    }
}
