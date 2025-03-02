using ArticleCatalog.Infrastructure;
using Bookmarks.Infrastructure;
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
    .AddTokenAuthentication(builder.Configuration)
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