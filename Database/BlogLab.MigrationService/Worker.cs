using ArticleCatalog.Infrastructure.Persistence;
using Bookmarks.Infrastructure.Persistence;
using Common.Infrastructure.Persistence;
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
        var initializers = scope.ServiceProvider.GetServices<IDbInitializer>();

        foreach (var initializer in initializers)
        {
            var name = initializer.Name;
            try
            {
                logger.LogInformation("Migrating {DbContext}...", name);

                await initializer.Initialize(stoppingToken);

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
}
