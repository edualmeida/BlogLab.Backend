using Microsoft.EntityFrameworkCore;

public abstract class BaseDbContext<TContext> : DbContext where TContext : DbContext
{
    protected BaseDbContext(DbContextOptions<TContext> options)
        : base(options)
    {
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // eventDispatcher -> Dispatch(domainEvent from entity);
        // for example we could implement entity.Events to communicate between domains

        return await base.SaveChangesAsync(cancellationToken);
    }
}