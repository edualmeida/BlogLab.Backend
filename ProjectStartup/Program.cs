using ArticleCatalog.Application;
using ArticleCatalog.Infrastructure;
using ArticleCatalog.Web;
using Bookmarks.Application;
using Bookmarks.Infrastructure;
using Bookmarks.Web;
using Common.Infrastructure;
using Common.Web;
using Identity.Application;
using Identity.Infrastructure;
using Identity.Web;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

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

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

app
    .UseSerilogRequestLogging()
    .UseWebService(app.Environment)
    .InitializeDatabase(builder.Configuration);

await app.RunAsync();