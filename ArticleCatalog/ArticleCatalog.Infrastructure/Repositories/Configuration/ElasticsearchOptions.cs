namespace ArticleCatalog.Infrastructure.Repositories.Configuration;
public sealed class ElasticsearchOptions
{
    public string NodeUri { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string IndexName { get; set; } = string.Empty;
}
