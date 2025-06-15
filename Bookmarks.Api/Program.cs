using Bookmarks.Application;
using Bookmarks.Domain;
using Bookmarks.Infrastructure;
using Common.Web;

namespace Bookmarks.Api;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        builder.Services
            .AddBookmarksDomain()
            .AddBookmarksApplication(builder.Configuration)
            .AddBookmarksInfrastructure(builder.Configuration)
            .AddBookmarksWebComponents();

        var app = builder.Build();

        app.UseCommonWebComponents();

        app.Run();
    }
}
