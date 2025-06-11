using Bookmarks.Domain;
using Bookmarks.Application;
using Bookmarks.Infrastructure;

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

        app.MapDefaultEndpoints();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
