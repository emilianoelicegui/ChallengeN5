using ChallengeN5.Repositories.Dto;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace ChallengeN5.Api.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                _logger.LogInformation($"Init request ..");

                await _next(context);

                _logger.LogInformation($"Finish ok request ..");

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Erorr: {ex.Message}");
                await HandleGlobalExceptionAsync(context, ex);
            }
        }

        private static Task HandleGlobalExceptionAsync(HttpContext context, Exception exception)
        {
            string message = string.Empty;
            var statusCode = (int)HttpStatusCode.BadRequest;

            if (exception is ValidationException)
            {
                message = exception.Message;
                statusCode = (int)HttpStatusCode.UnprocessableEntity;
            }

            if (exception is ApplicationException)
            {
                message = "Error internal server";
                statusCode = (int)HttpStatusCode.InternalServerError;
            }

            if (message.IsNullOrEmpty())
                message = exception.Message;

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsJsonAsync(new BaseResponse(message));
        }
    }
}
