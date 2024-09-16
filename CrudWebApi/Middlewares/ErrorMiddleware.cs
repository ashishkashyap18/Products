using System.Net;

namespace CrudWebApi.Middlewares
{
    public class ErrorMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorMiddleWare> _logger;

        public ErrorMiddleWare(RequestDelegate next,ILogger<ErrorMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }catch(Exception ex)
            {
                _logger.LogError(ex, "An error occured");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType="application/json";
            context.Response.StatusCode= (int)HttpStatusCode.InternalServerError;

            var response = new { message = "Internal Server Error", details = exception.Message };
            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
