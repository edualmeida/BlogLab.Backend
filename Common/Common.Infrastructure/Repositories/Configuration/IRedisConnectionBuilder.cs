using StackExchange.Redis;

namespace Common.Infrastructure.Repositories.Configuration;
public interface IRedisConnectionBuilder
{
    IConnectionMultiplexer BuildConnection();
}
