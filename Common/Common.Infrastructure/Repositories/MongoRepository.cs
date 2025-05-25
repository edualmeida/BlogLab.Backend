using Common.Domain.Models;
using Common.Infrastructure.Repositories.Configuration;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Common.Infrastructure.Repositories;
public abstract class MongoRepository<T> where T : Entity
{
    private readonly IMongoCollection<T> _collection;

    protected MongoRepository(IMongoDatabaseOptions options, string? collectionName = null)
    {
        var client = new MongoClient(options.ConnectionString);
        var database = client.GetDatabase(options.DatabaseName);

        _collection = database.GetCollection<T>(collectionName ?? typeof(T).Name);
    }

    public async Task<List<T>> FindAsync(Expression<Func<T, bool>> filter) =>
        await _collection.Find(filter).ToListAsync();

    public async Task<List<T>> GetAllAsync() =>
        await _collection.Find(_ => true).ToListAsync();

    public async Task<T?> GetByIdAsync(Guid id) =>
        await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(T entity) =>
        await _collection.InsertOneAsync(entity);

    public async Task UpdateAsync(Guid id, T entity) =>
        await _collection.ReplaceOneAsync(x => x.Id == id, entity);

    public async Task DeleteAsync(Guid id) =>
        await _collection.DeleteOneAsync(x => x.Id == id);
}
