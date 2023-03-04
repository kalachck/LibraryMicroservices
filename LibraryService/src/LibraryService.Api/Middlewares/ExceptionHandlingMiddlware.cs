using LibrarySevice.BussinesLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySevice.Api.Middlewares
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
            catch (Exception)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}
