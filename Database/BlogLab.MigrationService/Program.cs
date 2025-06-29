using ArticleCatalog.Infrastructure.Persistence;
using Bookmarks.Infrastructure.Persistence;
using Common.Infrastructure;
using Identity.Infrastructure.Persistence;

namespace BlogLab.MigrationService;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        builder.AddServiceDefaults();
        builder.Services.AddHostedService<Worker>();

        builder.Services.AddDatabase<AppIdentityDbContext>(
            builder.Configuration,
            InfrastructureConstants.DefaultConnectionStringName);

        builder.Services.AddDatabase<BookmarksDbContext>(
            builder.Configuration,
            InfrastructureConstants.DefaultConnectionStringName);

        builder.Services.AddDatabase<ArticleCatalogDbContext>(
            builder.Configuration, 
            InfrastructureConstants.DefaultConnectionStringName);

        var host = builder.Build();
        host.Run();
    }
}