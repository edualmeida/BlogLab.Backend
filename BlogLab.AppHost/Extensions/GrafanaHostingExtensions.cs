using Aspire.Hosting.ApplicationModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BlogLab.AppHost.Extensions;
public static class GrafanaHostingExtensions
{
    public static IResourceBuilder<ContainerResource> AddGrafanaWithPrometheus(
        this IDistributedApplicationBuilder builder,
        EndpointReference prometheusEndpoint)
    {
        return builder
            .AddContainer("grafana", "grafana/grafana")
            .WithBindMount("../grafana/config", "/etc/grafana", isReadOnly: true)
            .WithBindMount("../grafana/dashboards", "/var/lib/grafana/dashboards", isReadOnly: true)
            .WithEnvironment("PROMETHEUS_ENDPOINT", prometheusEndpoint)
            .WithHttpEndpoint(targetPort: 3000, name: "http");
    }
}