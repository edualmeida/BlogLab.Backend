using ArticleCatalog.Domain.Telemetry;
using System.Diagnostics.Metrics;

namespace ArticleCatalog.Infrastructure.Telemetry;
public class ArticleCatalogMetrics  : IArticleCatalogMetrics
{
    public static string MeterName => "ArticleCatalog.Metrics";

    private readonly Counter<int> AuthorsApiCallCounter;
    private readonly Histogram<double> AuthorsApiCallDuration; // Duration in milliseconds

    public ArticleCatalogMetrics(IMeterFactory meterFactory)
    {
        var meter = meterFactory.Create(MeterName);

        AuthorsApiCallCounter = meter
            .CreateCounter<int>("authors_api_call_count", 
                description: "Number of api calls done to Authors API");

        AuthorsApiCallDuration = meter
            .CreateHistogram<double>(
                "authors_api_call_duration_ms", 
                "ms", 
                "Duration of Authors API calls in milliseconds");
    }

    public void IncrementApiCallCount()
    {
        AuthorsApiCallCounter.Add(1);
    }

    public void RecordAuthorsApiCallDuration(double durationMs)
    {
        AuthorsApiCallDuration.Record(durationMs);
    }

    public async Task<T> MeasureAuthorsApiCallAsync<T>(Func<Task<T>> apiCall)
    {
        var sw = System.Diagnostics.Stopwatch.StartNew();
        try
        {
            return await apiCall();
        }
        finally
        {
            sw.Stop();
            AuthorsApiCallDuration.Record(sw.Elapsed.TotalMilliseconds);
        }
    }
}
