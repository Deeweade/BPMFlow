using BPFlow.API.Models.Exceptions;
using BPFlow.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace BPFlow.API.Middlewares;

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

    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        httpContext.Response.ContentType = "application/json";

        httpContext.Response.StatusCode = exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            DbUpdateException => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };

        var response = new
        {
            errors = new List<string>()
        };

        var innerException = exception;

        while (innerException is not null)
        {
            response.errors.Add(innerException.Message);

            innerException = innerException.InnerException;
        }

        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
