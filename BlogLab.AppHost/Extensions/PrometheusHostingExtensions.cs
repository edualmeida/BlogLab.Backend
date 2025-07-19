namespace BlogLab.AppHost.Extensions;
internal static class PrometheusHostingExtensions
{
    public static IResourceBuilder<ContainerResource> AddPrometheus(
        this IDistributedApplicationBuilder builder)
    {
        //•	Without tag:
        // The container runtime(Docker) will pull the latest tag by default.
        return builder
            .AddContainer("prometheus", "prom/prometheus", "v3.2.1") // tag This ensures repeatable, predictable builds and avoids surprises from upstream changes.
            .WithBindMount("../prometheus", "/etc/prometheus", isReadOnly: true)
            //.WithVolume("prometheus-config", "./prometheus", "/etc/prometheus")
            .WithArgs("--web.enable-otlp-receiver", "--config.file=/etc/prometheus/prometheus.yml")
            .WithHttpEndpoint(targetPort: 9090, name: "prometheus-ui");
    }
}
