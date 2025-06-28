using Common.Infrastructure;

namespace BlogLab.AppHost.Extensions;
public static class RedisHostingExtensions
{
    public static IResourceBuilder<RedisResource> AddRedisCache(
        this IDistributedApplicationBuilder builder)
    {
        return builder
            .AddRedis(InfrastructureConstants.RedisCacheName)
            .WithDataVolume(isReadOnly: false)
            .WithRedisInsight();
    }
}