using Common.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Common.Web.Middleware;
public class ValidationExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is ModelValidationException validationException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            //var problemDetails = new ProblemDetails
            //{
            //    Title = "An error occurred",
            //    Status = StatusCodes.Status400BadRequest,
            //    Detail = "One or more validation errors occurred.",
            //    Type = exception.GetType().Name,
            //    Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}",
            //    Extensions = { ["errors"] = validationException.Errors }
            //};

            await context.Response.WriteAsJsonAsync(validationException.Errors, cancellationToken);

            return true;
        }

        return false;
    }
}