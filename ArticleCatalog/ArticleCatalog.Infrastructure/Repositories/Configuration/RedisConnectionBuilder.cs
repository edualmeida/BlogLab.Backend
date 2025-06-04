using ArticleCatalog.Infrastructure.Repositories.Configuration;
using Common.Infrastructure.Repositories.Configuration;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Common.Infrastructure.Repositories;
public class RedisConnectionBuilder(
    IOptions<ArticleCacheOptions> options) : IRedisConnectionBuilder
{
    public IConnectionMultiplexer BuildConnection()
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentNullException.ThrowIfNull(options.Value);

        return ConnectionMultiplexer.Connect(options.Value.ConnectionString);
    }
}
