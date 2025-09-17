using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace manager_retals.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            (HttpStatusCode statusCode, string message) = exception switch
            {
                ArgumentNullException => (HttpStatusCode.BadRequest, "Dados inválidos"),
                AccessViolationException => (HttpStatusCode.BadRequest, exception.Message),
                //BusinessRuleException => HttpStatusCode.BadRequest,
                _ => (HttpStatusCode.InternalServerError, "")
            };

            var response = new
            {
                Message = message,
            };

            var payload = JsonSerializer.Serialize(response);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(payload);
        }
    }
}
