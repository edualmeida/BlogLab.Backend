using Common.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Common.Web.Middleware;
public class GlobalExceptionHandler(IHostEnvironment env, ILogger<GlobalExceptionHandler> logger)
    : IExceptionHandler
{
    private const string UnhandledExceptionMsg = "An unhandled exception has occurred while executing the request.";

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, httpContext.TraceIdentifier);

        var exceptionHandlerFeature = httpContext.Features.Get<IExceptionHandlerFeature>();
        // TODO: remove custom exception handling because we are going to use Result pattern with
        // custom ToActionResult method in the controller
        var statusCode = exceptionHandlerFeature!.Error switch
        {
            NotFoundException => StatusCodes.Status404NotFound,
            BadRequestException => StatusCodes.Status400BadRequest,
            HttpRequestException ex => (int)ex.StatusCode!.Value,
            _ => StatusCodes.Status500InternalServerError
        };

        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/problem+json";

        var problemDetails = CreateProblemDetails(httpContext, exception, exceptionHandlerFeature);
        await httpContext.Response.WriteAsync(ToJson(problemDetails), cancellationToken);

        return true;
    }

    private ProblemDetails CreateProblemDetails(
        HttpContext httpContext, 
        Exception exception, 
        IExceptionHandlerFeature? exceptionHandlerFeature)
    {
        //var errorCode = exception.GetErrorCode(); maybe we will have custom error codes in the future
        var statusCode = httpContext.Response.StatusCode;
        var reasonPhrase = ReasonPhrases.GetReasonPhrase(statusCode);
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = string.IsNullOrEmpty(reasonPhrase) ? UnhandledExceptionMsg : reasonPhrase,
            Extensions =
            {
                ["errorType"] = ProblemDetailErrorType.Unexpected
            }
        };

        if (!env.IsDevelopment())
        {
            return problemDetails;
        }

        problemDetails.Instance = httpContext.Request.Path;
        problemDetails.Detail = exception.ToString();
        problemDetails.Extensions["traceId"] = Activity.Current?.Id;
        problemDetails.Extensions["requestId"] = httpContext.TraceIdentifier;
        problemDetails.Extensions["data"] = exception.Data;
        problemDetails.Extensions["nodeId"] = Environment.MachineName;
        problemDetails.Extensions["exception"] = new
        {
            httpContext.Request.Headers,
            Path = httpContext.Request.Path.ToString(),
            Endpoint = exceptionHandlerFeature?.Endpoint?.ToString(),
            exceptionHandlerFeature?.RouteValues,
        };

        return problemDetails;
    }

    private string ToJson(ProblemDetails problemDetails)
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            return JsonSerializer.Serialize(problemDetails, options);
        }
        catch (Exception ex)
        {
            const string msg = $"An exception has occurred while serializing {nameof(ProblemDetails)} to JSON";
            logger.LogError(ex, msg);
        }

        return string.Empty;
    }
}