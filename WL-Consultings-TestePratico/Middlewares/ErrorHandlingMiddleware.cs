using System.ComponentModel.DataAnnotations;
using WL_Consultings_TestePratico.Models.DTOs.Error;
using WL_Consultings_TestePratico.Models.Exceptions;

namespace WL_Consultings_TestePratico.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
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

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var result = ex switch
            {
                ValidationException validationEx => new ErrorResponse(400, validationEx.Message),
                BusinessException => new ErrorResponse(400, ex.Message),
                NotFoundException => new ErrorResponse(404, ex.Message),
                UnauthorizedAccessException => new ErrorResponse(403, ex.Message),
                ArgumentException => new ErrorResponse(400, ex.Message),
                _ => new ErrorResponse(500, $"Erro interno do servidor")
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = result.Status;
            return context.Response.WriteAsJsonAsync(result);
        }

    }
}
