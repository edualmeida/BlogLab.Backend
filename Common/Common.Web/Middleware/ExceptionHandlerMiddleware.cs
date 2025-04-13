using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Common.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Common.Web.Middleware;
public class ExceptionHandlerMiddleware(
    RequestDelegate next, 
    ILogger<ExceptionHandlerMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;

        logger.LogError(exception, context.TraceIdentifier);
        switch (exception)
        {
            case ModelValidationException modelValidationException:
                code = HttpStatusCode.BadRequest;
                result = SerializeObject(new
                {
                    ValidationDetails = true,
                    modelValidationException.Errors
                });
                break;
            case NullReferenceException _:
                code = HttpStatusCode.BadRequest;
                result = SerializeObject(new[] { "Invalid request." });
                break;
            case NotFoundException _:
                code = HttpStatusCode.NotFound;
                break;
            case HttpRequestException ex:
                code = ex.StatusCode!.Value;
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if (string.IsNullOrEmpty(result))
        {
            var error = exception.Message + exception.InnerException?.Message;

            if (exception is BaseDomainException baseDomainException)
            {
                error = baseDomainException.Error;
            }

            result = SerializeObject(new[] { error });
        }

        return context.Response.WriteAsync(result);
    }

    private static string SerializeObject(object obj)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        return JsonSerializer.Serialize(obj, options);
    }
}