using Common.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Common.Web.Middleware;
public class ValidationExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is ModelValidationException validationException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            httpContext.Response.ContentType = "application/json";

            var errors = validationException.Errors
                .SelectMany(x => x.Value)
                .Distinct()
                .ToArray() ?? [];

            await httpContext.Response.WriteAsJsonAsync(new ProblemDetails()
            {
                Title = "Validation error",
                Detail = string.Join(", ", errors),
                Status = StatusCodes.Status400BadRequest,
                Extensions = { 
                    ["errorType"] = ProblemDetailErrorType.Validation,
                    ["validationErrors"] = validationException.Errors
                },
            }, cancellationToken);

            return true;
        }

        return false;
    }
}