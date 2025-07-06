using Common.Domain.Telemetry;

namespace ArticleCatalog.Domain.Telemetry;
public interface IArticleCatalogMetrics: ITelemetry
{
    void IncrementApiCallCount();
    Task<T> MeasureAuthorsApiCallAsync<T>(Func<Task<T>> apiCall);
}