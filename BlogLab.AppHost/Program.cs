using BlogLab.AppHost.Extensions;
using BlogLab.AppHost.OpenTelemetryCollector;

var builder = DistributedApplication.CreateBuilder(args);

var prometheusEndpoint = builder.AddPrometheus().GetEndpoint("http");
var grafana = builder.AddGrafanaWithPrometheus(prometheusEndpoint);

builder.AddOpenTelemetry(prometheusEndpoint);

var redis = builder.AddRedisCache();
var blogLabDatabase = builder.AddPostgresWithMigration();

var bookmarks = builder.AddProject<Projects.Bookmarks_Api>("bookmarks");
var identity = builder.AddProject<Projects.Identity_Api>("identity");

builder.AddProject<Projects.Comments_Api>("comments");

builder.AddProject<Projects.ArticleCatalog_Api>("articles")
    .WithExternalHttpEndpoints()
    .WithReference(bookmarks)
    .WithReference(identity)
    .WithReference(redis)
    .WaitFor(bookmarks)
    .WaitFor(identity)
    .WithEnvironment("GRAFANA_URL", grafana.GetEndpoint("http"));

builder.Build().Run();
