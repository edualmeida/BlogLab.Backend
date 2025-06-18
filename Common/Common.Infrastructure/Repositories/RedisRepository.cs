using Common.Domain;
using StackExchange.Redis;
using System.Text.Json;

namespace Common.Infrastructure.Repositories;
public sealed class RedisRepository : ICacheRepository
{
    private readonly IDatabase _db;
    private readonly JsonSerializerOptions _serializerOptions;

    public RedisRepository(IConnectionMultiplexer connectionMultiplexer)
    {
        if (connectionMultiplexer is null || !connectionMultiplexer.IsConnected)
            throw new InvalidOperationException("Failed to connect to Redis server.");

        _db = connectionMultiplexer.GetDatabase();
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
    {
        var json = JsonSerializer.Serialize(value, _serializerOptions);
        await _db.StringSetAsync(key, json, expiry);
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var value = await _db.StringGetAsync(key);
        if (value.IsNullOrEmpty)
            return default;

        return JsonSerializer.Deserialize<T>(value!, _serializerOptions);
    }

    public async Task<bool> ExistsAsync(string key) =>
        await _db.KeyExistsAsync(key);

    public async Task<bool> DeleteAsync(string key) =>
        await _db.KeyDeleteAsync(key);
}

