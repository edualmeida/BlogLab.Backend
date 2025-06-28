using ArticleCatalog.Infrastructure.Persistence;
using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BlogLab.MigrationService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        builder.AddServiceDefaults();
        builder.Services.AddHostedService<Worker>();
        builder.Services.AddDatabase<ArticleCatalogDbContext>(builder.Configuration, "bloglab");

        var host = builder.Build();
        host.Run();
    }
}