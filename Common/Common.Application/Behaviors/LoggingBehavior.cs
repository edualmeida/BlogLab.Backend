using MediatR;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Common.Application.Behaviors;
public class LoggingBehavior<TRequest, TResponse>(ILogger<TRequest> logger)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        var requestName = request.GetType().Name;
        var requestGuid = Guid.NewGuid().ToString();

        var requestNameWithGuid = $"{requestName} [{requestGuid}]";

        logger.LogDebug("[START] {RequestNameWithGuid}", requestNameWithGuid);
        TResponse response;

        var stopwatch = Stopwatch.StartNew();
        try
        {
            try
            {
                logger.LogDebug("[PROPS] {RequestNameWithGuid} {JsonRequest}", 
                    requestNameWithGuid, JsonSerializer.Serialize(request));
            }
            catch (NotSupportedException ex)
            {
                logger.LogError(ex, "[Serialization ERROR] {RequestNameWithGuid} Could not serialize the request.",
                    requestNameWithGuid);
            }

            response = await next();
        }
        finally
        {
            stopwatch.Stop();
            logger.LogDebug("[END] {RequestNameWithGuid}; Execution time={ElapsedMilliseconds}ms",
                requestNameWithGuid, stopwatch.ElapsedMilliseconds);
        }

        return response;
    }

}