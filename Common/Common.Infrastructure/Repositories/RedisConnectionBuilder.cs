using Common.Infrastructure.Repositories.Configuration;
using StackExchange.Redis;

namespace Common.Infrastructure.Repositories;
public class RedisConnectionBuilder : IRedisConnectionBuilder
{
    private readonly IRedisOptions _options;

    public RedisConnectionBuilder(IRedisOptions options)
    {
        _options = options;
    }

    public IConnectionMultiplexer BuildConnection()
    {
        return ConnectionMultiplexer.Connect(_options.ConnectionString);
    }
}
