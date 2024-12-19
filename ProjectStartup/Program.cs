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

builder.Services
    .AddTokenAuthentication(builder.Configuration)
    .AddModelBinders()
    .AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new() { Title = "Blog API", Version = "v1" });
    })
    .AddHttpClient();

var app = builder.Build();

app
    .UseWebService(app.Environment)
    .Initialize();

app.Run();