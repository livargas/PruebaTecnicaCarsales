using System.Net;
using System.Text.Json;
using PruebaTecnicaCarsales.Models;

namespace PruebaTecnicaCarsales.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error inesperado.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            
            context.Response.StatusCode = exception switch
            {
                KeyNotFoundException => (int) HttpStatusCode.NotFound,
                ArgumentException => (int) HttpStatusCode.BadRequest,  
                _ => (int) HttpStatusCode.InternalServerError
            };

            var message = exception switch
            {
                KeyNotFoundException => "¡Wubba Lubba Dub Dub!, El recurso solicitado no se encontró.",
                ArgumentException => "¡Wubba Lubba Dub Dub!, Los parámetros de la solicitud son inválidos.",
                _ => "¡Wubba Lubba Dub Dub!, Ocurrió un error interno en el servidor."
            };

            if (_env.IsDevelopment())
            {
                message = exception.Message;
            }

            var response = new ErrorResponse
            {
                StatusCode = context.Response.StatusCode,
                Message = message,
                Details = _env.IsDevelopment() ? exception.StackTrace : null
            };

            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }
    }
}
