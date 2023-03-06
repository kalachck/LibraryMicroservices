using System.Net;

namespace LibraryService.Api.Middlewares
{
    public class ExceptionHandlingMiddlware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddlware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await context.Response.WriteAsync(ex.Message);
            }
        }
    }
}
