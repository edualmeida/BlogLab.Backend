using Common.Domain;
using Common.Infrastructure.Repositories.Configuration;
using StackExchange.Redis;
using System.Text.Json;

namespace Common.Infrastructure.Repositories;
public sealed class RedisRepository<T> : ICacheRepository<T>
    where T : class
{
    private readonly IDatabase _db;
    private readonly JsonSerializerOptions _serializerOptions;

    public RedisRepository(IRedisConnectionBuilder redisConnectionBuilder)
    {
        if (redisConnectionBuilder is null)
            throw new ArgumentNullException(nameof(redisConnectionBuilder));

        var connection = redisConnectionBuilder.BuildConnection();

        if (connection is null || !connection.IsConnected)
            throw new InvalidOperationException("Failed to connect to Redis server.");

        _db = connection.GetDatabase();
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };
    }

    public async Task SetAsync(string key, T value, TimeSpan? expiry = null)
    {
        var json = JsonSerializer.Serialize(value, _serializerOptions);
        await _db.StringSetAsync(key, json, expiry);
    }

    public async Task<T?> GetAsync(string key)
    {
        var value = await _db.StringGetAsync(key);
        if (value.IsNullOrEmpty)
            return null;

        return JsonSerializer.Deserialize<T>(value!, _serializerOptions);
    }

    public async Task<bool> ExistsAsync(string key) =>
        await _db.KeyExistsAsync(key);

    public async Task<bool> DeleteAsync(string key) =>
        await _db.KeyDeleteAsync(key);
}

