using ArticleCatalog.Application;
using ArticleCatalog.Infrastructure;
using ArticleCatalog.Web;
using ArticleCatalog.Domain;
using Bookmarks.Application;
using Bookmarks.Infrastructure;
using Bookmarks.Web;
using Bookmarks.Domain;
using Common.Infrastructure;
using Common.Web;
using Comments.Infrastructure;
using Comments.Domain;
using Comments.Application;
using Comments.Web;
using Identity.Application;
using Identity.Infrastructure;
using Identity.Web;
using Identity.Domain;
using ProjectStartup;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

builder
    .Services
    .AddArticleCatalogDomain()
    .AddArticleCatalogApplication(builder.Configuration)
    .AddArticleCatalogInfrastructure(builder.Configuration)
    .AddArticleCatalogWebComponents();

builder
    .Services
    .AddBookmarksDomain()
    .AddBookmarksApplication(builder.Configuration)
    .AddBookmarksInfrastructure(builder.Configuration)
    .AddBookmarksWebComponents();

builder
    .Services
    .AddCommentsDomain()
    .AddCommentsApplication(builder.Configuration)
    .AddCommentsInfrastructure(builder.Configuration)
    .AddCommentsWebComponents();

builder
    .Services
    .AddIdentityDomain()
    .AddIdentityApplication(builder.Configuration)
    .AddIdentityInfrastructure(builder.Configuration)
    .AddIdentityWebComponents();

builder
    .Services
    .AddAuthenticationHandlers(builder.Configuration)
    .AddModelBinders()
    .AddSwagger()
    .AddHttpClient();

builder.Services.AddExceptionHandlers();

// Enable Serilog self-logging to the console for troubleshooting
Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine($"[ELK]:{msg}"));

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

app
    .SetupWebApplication(app.Environment)
    .InitializeDatabase(builder.Configuration);

app.MapPrometheusScrapingEndpoint();

await app.RunAsync();