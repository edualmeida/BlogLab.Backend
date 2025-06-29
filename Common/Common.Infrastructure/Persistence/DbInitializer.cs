using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;

namespace Common.Infrastructure.Persistence;
public abstract class DbInitializer : IDbInitializer
{
    private readonly DbContext dbContext;
    private readonly IEnumerable<IInitialData> initialDataProviders;

    protected internal DbInitializer(DbContext db, string name)
    {
        this.dbContext = db;
        initialDataProviders = new List<IInitialData>();
        Name = name;
    }

    protected internal DbInitializer(
        DbContext dbContext,
        IEnumerable<IInitialData> initialDataProviders, 
        string name)
        : this(dbContext, name)
        => this.initialDataProviders = initialDataProviders;

    public virtual string Name { get; }
    public virtual async Task Initialize(CancellationToken stoppingToken)
    {
        await EnsureDatabaseAsync(dbContext, stoppingToken);
        await RunMigrationAsync(dbContext, stoppingToken);

        foreach (var initialDataProvider in initialDataProviders)
        {
            if (DataSetIsEmpty(initialDataProvider.EntityType))
            {
                var data = initialDataProvider.GetData();

                foreach (var entity in data)
                {
                    dbContext.Add(entity);
                }
            }
        }

        await dbContext.SaveChangesAsync();
    }

    private static async Task EnsureDatabaseAsync(
        DbContext dbContext, CancellationToken cancellationToken)
    {
        var dbCreator = dbContext.GetService<IRelationalDatabaseCreator>();

        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            if (!await dbCreator.ExistsAsync(cancellationToken))
            {
                await dbCreator.CreateAsync(cancellationToken);
            }
        });
    }

    private static async Task RunMigrationAsync(
        DbContext dbContext, CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await dbContext.Database.MigrateAsync(cancellationToken);
        });
    }

    private bool DataSetIsEmpty(Type type)
    {
        var setMethod = typeof(DbInitializer)
            .GetMethod(nameof(GetSet), BindingFlags.Instance | BindingFlags.NonPublic)!
            .MakeGenericMethod(type);

        var set = setMethod.Invoke(this, Array.Empty<object>());

        var countMethod = typeof(Queryable)
            .GetMethods()
            .First(m => m.Name == nameof(Queryable.Count) && m.GetParameters().Length == 1)
            .MakeGenericMethod(type);

        var result = (int)countMethod.Invoke(null, [set])!;

        return result == 0;
    }

    private DbSet<TEntity> GetSet<TEntity>()
        where TEntity : class
        => dbContext.Set<TEntity>();
}