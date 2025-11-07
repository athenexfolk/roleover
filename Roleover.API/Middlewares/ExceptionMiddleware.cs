using System.Net;
using System.Text.Json;
using Roleover.Application.Exceptions;

namespace Roleover.API.Middlewares;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/problem+json";
            int statusCode = ex switch
            {
                BadRequestException => (int)HttpStatusCode.BadRequest,
                UnauthorizedException => (int)HttpStatusCode.Unauthorized,
                ForbiddenException => (int)HttpStatusCode.Forbidden,
                NotFoundException => (int)HttpStatusCode.NotFound,
                ConflictException => (int)HttpStatusCode.Conflict,
                _ => (int)HttpStatusCode.InternalServerError
            };
            context.Response.StatusCode = statusCode;

            string title = statusCode switch
            {
                (int)HttpStatusCode.BadRequest => "Bad Request",
                (int)HttpStatusCode.Unauthorized => "Unauthorized",
                (int)HttpStatusCode.Forbidden => "Forbidden",
                (int)HttpStatusCode.NotFound => "Not Found",
                (int)HttpStatusCode.Conflict => "Conflict",
                _ => "Internal Server Error"
            };

            var problemDetails = new
            {
                type = "https://tools.ietf.org/html/rfc7807",
                title,
                status = statusCode,
                detail = ex.Message,
                instance = context.Request.Path
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
        }
    }
}
