
using ArticleCatalog.Application;
using ArticleCatalog.Domain;
using ArticleCatalog.Infrastructure;
using ArticleCatalog.Infrastructure.Persistence;
using Common.Infrastructure;
using Common.Web;

namespace ArticleCatalog.Api;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        builder.Services
            .AddArticleCatalogDomain()
            .AddArticleCatalogApplication(builder.Configuration)
            .AddArticleCatalogInfrastructure(builder.Configuration)
            .AddArticleCatalogWebComponents();

        builder.AddHostingInfrastructure<ArticleCatalogDbContext>();

        var app = builder.Build();

        app.UseCommonWebComponents();

        app.Run();
    }
}
