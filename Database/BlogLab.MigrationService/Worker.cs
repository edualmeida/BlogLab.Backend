using ArticleCatalog.Infrastructure.Persistence;
using Bookmarks.Infrastructure.Persistence;
using Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using OpenTelemetry.Trace;
using System.Diagnostics;

namespace BlogLab.MigrationService;

public class Worker(
    ILogger<Worker> logger,
    IServiceProvider serviceProvider,
    IHostEnvironment hostEnvironment,
    IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    private readonly ActivitySource _activitySource = new(hostEnvironment.ApplicationName);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var activity = _activitySource.StartActivity(hostEnvironment.ApplicationName, ActivityKind.Client);
        using var scope = serviceProvider.CreateScope();
        var dbContexts = new List<Type>
            {
                typeof(AppIdentityDbContext),
                typeof(BookmarksDbContext),
                typeof(ArticleCatalogDbContext),
            };

        foreach (var contextType in dbContexts)
        {
            var name = contextType.Name;
            try
            {
                var dbContext = (DbContext)scope.ServiceProvider.GetRequiredService(contextType);

                logger.LogInformation("Migrating {DbContext}...", name);

                await EnsureDatabaseAsync(dbContext, stoppingToken);
                await RunMigrationAsync(dbContext, stoppingToken);

                logger.LogInformation("{DbContext} migration complete.", name);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{DbContext} migration error.", name);
                activity?.RecordException(ex);
                throw;
            }
        }

        logger.LogInformation("All migrations complete.");

        hostApplicationLifetime.StopApplication();
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
}
