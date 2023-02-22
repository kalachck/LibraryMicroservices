using BorrowService.Borrowings.Exceptions;
using System.Net;

namespace BorrowService.Api.Middlewares
{
    public class NotFoundExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public NotFoundExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;

                await context.Response.WriteAsync(ex.Message);
            }
        }
    }
}
