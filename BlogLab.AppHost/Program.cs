var builder = DistributedApplication.CreateBuilder(args);

var grafana = builder.AddContainer("grafana", "grafana/grafana")
    .WithVolumeMount("../grafana/config", "/etc/grafana")
    .WithVolumeMount("../grafana/dashboards", "/var/lib/grafana/dashboards")
    .WithEndpoint(containerPort: 3000, hostPort: 3000, name: "grafana-http", scheme: "http");

builder.AddContainer("prometheus", "prom/prometheus")
    .WithVolumeMount("../prometheus", "/etc/prometheus")
    .WithEndpoint(9090, hostPort: 9090);

var bookmarks = builder.AddProject<Projects.Bookmarks_Api>("bookmarks");
var identity = builder.AddProject<Projects.Identity_Api>("identity");

builder.AddProject<Projects.Comments_Api>("comments");

builder.AddProject<Projects.ArticleCatalog_Api>("articles")
    .WithExternalHttpEndpoints()
    .WithReference(bookmarks)
    .WithReference(identity)
    .WaitFor(bookmarks)
    .WaitFor(identity);

builder.Build().Run();
