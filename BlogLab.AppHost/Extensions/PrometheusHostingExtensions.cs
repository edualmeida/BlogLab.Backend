namespace BlogLab.AppHost.Extensions;
internal static class PrometheusHostingExtensions
{
    public static IResourceBuilder<ContainerResource> AddPrometheus(
        this IDistributedApplicationBuilder builder)
    {
        return builder
            .AddContainer("prometheus", "prom/prometheus", "v3.2.1")
            .WithBindMount("../prometheus", "/etc/prometheus", isReadOnly: true)
            .WithArgs("--web.enable-otlp-receiver", "--config.file=/etc/prometheus/prometheus.yml")
            .WithHttpEndpoint(targetPort: 9090, name: "http");
    }
}
