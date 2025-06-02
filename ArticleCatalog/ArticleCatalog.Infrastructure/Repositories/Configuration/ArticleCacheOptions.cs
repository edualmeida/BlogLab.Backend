using Common.Infrastructure.Repositories.Configuration;

namespace ArticleCatalog.Infrastructure.Repositories.Configuration;
public class ArticleCacheOptions : IRedisOptions
{
    public string ConnectionString { get; set; } = string.Empty;
}
