using Aspire.Hosting;
using BlogLab.AppHost.Extensions;
using BlogLab.AppHost.OpenTelemetryCollector;
using Common.Infrastructure;

var builder = DistributedApplication.CreateBuilder(args);

var prometheusEndpoint = builder.AddPrometheus().GetEndpoint("http");
var grafana = builder.AddGrafanaWithPrometheus(prometheusEndpoint);
var jaeger = builder.AddJaeger();

//builder.AddOpenTelemetryCollector(prometheusEndpoint);

var redis = builder.AddRedisCache();
var blogLabDatabase = builder.AddPostgresWithMigration();

var bookmarks = builder.AddProject<Projects.Bookmarks_Api>(InfrastructureConstants.BookmarksApiName);
var identity = builder.AddProject<Projects.Identity_Api>(InfrastructureConstants.IdentityApiName);

builder.AddProject<Projects.Comments_Api>(InfrastructureConstants.CommentsApiName);

builder.AddProject<Projects.ArticleCatalog_Api>("articles")
     //.WithExternalHttpEndpoints()
    .WithHttpEndpoint(
        //port: InfrastructureConstants.ArticlesApiPort, 
        targetPort: 6087, 
        name: "https-main")
    .WithReference(bookmarks)
    .WithReference(identity)
    .WithReference(redis)
    //.WithReference(jaeger)
    //.WithReference(prometheus)
    .WaitFor(bookmarks)
    .WaitFor(identity)
    .WithEnvironment("GRAFANA_URL", grafana.GetEndpoint("http"));

builder.Build().Run();
