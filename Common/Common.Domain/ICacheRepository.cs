namespace Common.Domain;
public interface ICacheRepository<T> where T : class
{
    Task SetAsync(string key, T value, TimeSpan? expiry = null);
    Task<T?> GetAsync(string key);
}
