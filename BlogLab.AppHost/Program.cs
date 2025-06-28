using BlogLab.AppHost.OpenTelemetryCollector;

var builder = DistributedApplication.CreateBuilder(args);

var prometheus = builder
    .AddContainer("prometheus", "prom/prometheus", "v3.2.1")
    .WithBindMount("../prometheus", "/etc/prometheus", isReadOnly: true)
    .WithArgs("--web.enable-otlp-receiver", "--config.file=/etc/prometheus/prometheus.yml")
    .WithHttpEndpoint(targetPort: 9090, name: "http");

var grafana = builder
    .AddContainer("grafana", "grafana/grafana")
    .WithBindMount("../grafana/config", "/etc/grafana", isReadOnly: true)
    .WithBindMount("../grafana/dashboards", "/var/lib/grafana/dashboards", isReadOnly: true)
    .WithEnvironment("PROMETHEUS_ENDPOINT", prometheus.GetEndpoint("http"))
    .WithHttpEndpoint(targetPort: 3000, name: "http");

builder
    .AddOpenTelemetryCollector("otelcollector", "../otelcollector/config.yaml")
    .WithEnvironment("PROMETHEUS_ENDPOINT", $"{prometheus.GetEndpoint("http")}/api/v1/otlp");

var redis = builder
    .AddRedis("BlogLabCache")
    .WithDataVolume(isReadOnly: false)
    .WithRedisInsight();

var username = builder.AddParameter("username", "postgres", secret: true);
var password = builder.AddParameter("password", "1q2w3e4r", secret: true);

var postgres = builder.AddPostgres("postgres", username, password)
    .WithHostPort(54285)
    .WithDataVolume(isReadOnly: false)
    //.WithPgWeb()
    .WithLifetime(ContainerLifetime.Persistent);

var blogLabDb = postgres.AddDatabase("bloglab");

var migrationService = builder.AddProject<Projects.BlogLab_MigrationService>("migrationservice")
    .WithReference(blogLabDb)
    .WaitFor(blogLabDb);

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
    .WithEnvironment("GRAFANA_URL", grafana.GetEndpoint("http")); ;

builder.Build().Run();
