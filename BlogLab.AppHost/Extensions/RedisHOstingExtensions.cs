namespace BlogLab.AppHost.Extensions;
public static class RedisHostingExtensions
{
    public static IResourceBuilder<RedisResource> AddRedis(this IDistributedApplicationBuilder builder)
    {
        return builder
            .AddRedis("BlogLabCache")
            .WithDataVolume(isReadOnly: false)
            .WithRedisInsight();
    }
}