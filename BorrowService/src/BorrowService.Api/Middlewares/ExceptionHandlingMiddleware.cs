using System.Net;

namespace BorrowService.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async void Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsync("Internal server error. The operation is not available");
            }
        }
    }
}
