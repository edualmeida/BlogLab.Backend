using Common.Infrastructure.Repositories.Configuration;

namespace ArticleCatalog.Infrastructure.Repositories.Configuration;
public sealed class ElasticsArticleOptions: IElasticsearchOptions
{
    public string NodeUri { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string IndexName { get; set; } = string.Empty;
}
