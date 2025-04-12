using Common.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure.Repositories;
public abstract class DataRepository<TDbContext, TEntity>(TDbContext db) 
    : IDomainRepository<TEntity>
    where TDbContext : DbContext
    where TEntity : Entity, IAggregateRoot
{
    protected TDbContext Data { get; } = db;

    protected IQueryable<TEntity> All() => Data.Set<TEntity>();

    protected IQueryable<TEntity> AllAsNoTracking() => All().AsNoTracking();

    public async Task Save(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        Data.Update(entity);
        await Data.SaveChangesAsync(cancellationToken);
    }
}
