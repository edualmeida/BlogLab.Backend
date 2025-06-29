using ArticleCatalog.Infrastructure.Persistence;
using Bookmarks.Infrastructure.Persistence;
using Common.Infrastructure;
using Common.Infrastructure.Persistence;
using Identity.Infrastructure.Persistence;
using Identity.Infrastructure;

namespace BlogLab.MigrationService;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        builder.AddServiceDefaults();

        builder.Services.AddHostedService<Worker>();

        builder.Services
            .AddDatabase<AppIdentityDbContext>(
                builder.Configuration,
                InfrastructureConstants.DefaultConnectionStringName)
            .AddIdentityStores()
            .AddTransient<IDbInitializer, AppIdentityDbInitializer>();

        builder.Services.AddDatabase<BookmarksDbContext>(
            builder.Configuration,
            InfrastructureConstants.DefaultConnectionStringName);

        builder.Services
            .AddDatabase<ArticleCatalogDbContext>(
                builder.Configuration, 
                InfrastructureConstants.DefaultConnectionStringName)
            .AddTransient<IDbInitializer, ArticleCatalogDbInitializer>();

        var host = builder.Build();
        host.Run();
    }
}