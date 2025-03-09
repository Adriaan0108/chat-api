using chat_api.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace chat_api.Helpers;

public class ApiAuthenticationMiddleware
{
    private readonly IOptions<AppSettings> _appSettings;
    private readonly RequestDelegate _next;

    public ApiAuthenticationMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
    {
        _next = next;
        _appSettings = appSettings;
    }

    public async Task InvokeAsync(HttpContext context, IAuthService authService)
    {
        // Always check for Project-ID header
        if (!context.Request.Headers.TryGetValue("Project-ID", out var projectId))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Required authentication headers are missing");
            return;
        }

        // Validate Project-ID
        if (_appSettings.Value.ProjectId != projectId)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Invalid Project-ID");
            return;
        }

        context.Items["ProjectId"] = projectId!;

        var endpoint = context.GetEndpoint();

        // Skip validation if the endpoint has [AllowAnonymous] attribute
        var allowAnonymous = endpoint?.Metadata
            .GetMetadata<AllowAnonymousAttribute>() != null;

        if (!allowAnonymous)
        {
            if (!context.Request.Headers.TryGetValue("User-Name", out var username) ||
                !context.Request.Headers.TryGetValue("User-Secret", out var secret))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Required authentication headers are missing");
                return;
            }


            try
            {
                // Validate credentials
                var user = await authService.IsAuthorized(projectId!, username!, secret!);

                // Store authenticated user info in HttpContext.Items
                context.Items["UserId"] = user.Id;
            }
            catch (Exception ex)
            {
                // Catch all exceptions and return a 401 Unauthorized with the error message
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync($"Authentication failed: {ex.Message}");
                return;
            }
        }


        // Continue to the next middleware or controller
        await _next(context);
    }
}