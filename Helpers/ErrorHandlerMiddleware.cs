using System.Net;
using System.Text.Json;
using chat_api.Exceptions;

namespace chat_api.Helpers;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (CustomException ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            // Map the error type to the appropriate HTTP status code
            response.StatusCode = ex.ErrorType switch
            {
                ErrorType.BadRequest => (int)HttpStatusCode.BadRequest,
                ErrorType.NotFound => (int)HttpStatusCode.NotFound,
                ErrorType.InternalServerError => (int)HttpStatusCode.InternalServerError,
                ErrorType.Forbidden => (int)HttpStatusCode.Forbidden,
                ErrorType.Unauthorized => (int)HttpStatusCode.Unauthorized,
                ErrorType.ServiceUnavailable => (int)HttpStatusCode.ServiceUnavailable,
                ErrorType.UnprocessableEntity => (int)HttpStatusCode.UnprocessableEntity,
                ErrorType.Conflict => (int)HttpStatusCode.Conflict,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var detailedMessage = ex.InnerException != null
                ? $"{ex.Message} | Inner Exception: {ex.InnerException.Message}"
                : ex.Message;

            var result = JsonSerializer.Serialize(new { message = detailedMessage });
            await response.WriteAsync(result);
        }
    }
}