using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using E_Commerce_APIs.Shared.Helpers;
using E_Commerce_APIs.Application.Exceptions;

namespace E_Commerce_APIs.API;

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

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        Result resultResponse;

        switch (exception)
        {
            case Application.Exceptions.ValidationException validationException:
                resultResponse = Result.ValidationFailure(
                    "Validation failed",
                    validationException.Errors,
                    (int)HttpStatusCode.BadRequest
                );
                break;

            case KeyNotFoundException:
                resultResponse = Result.Failure(
                    exception.Message,
                    (int)HttpStatusCode.NotFound
                );
                break;

            case UnauthorizedAccessException:
                resultResponse = Result.Failure(
                    "Unauthorized access",
                    (int)HttpStatusCode.Unauthorized
                );
                break;

            default:
                resultResponse = Result.Failure(
                    "An internal server error occurred",
                    (int)HttpStatusCode.InternalServerError
                );
                break;
        }

        response.StatusCode = resultResponse.StatusCode;
        var result = JsonSerializer.Serialize(resultResponse, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await response.WriteAsync(result);
    }
}
