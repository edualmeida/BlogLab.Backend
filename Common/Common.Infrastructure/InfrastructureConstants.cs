namespace Common.Infrastructure;
public static class InfrastructureConstants
{
    public const string ApiKeyOptions = "ApiKeyOptions";

    public const string OtelCollectorName = "otelcollector";
    public const string RedisCacheName = "redis";
    public const string BlogLabDatabaseName = "bloglab";
    public const string DefaultConnectionStringName = "bloglab";
    public const string IdentityApiName = "identity";
    public const string BookmarksApiName = "bookmarks";
    public const string ArticlesApiName = "articles";
    public const string CommentsApiName = "comments";
    public const string JaegerName = "jaeger";

    public const int ArticlesApiPort = 6087; // Default port for Articles API
    public const int JaegerPort = 6831; // Jaeger OTLP receiver port
    public const int OtelCollectorPort = 4317;
}