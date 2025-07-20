using Common.Infrastructure;

namespace BlogLab.AppHost.Extensions;
internal static class JaegerHostingExtensions
{
    public static IResourceBuilder<ContainerResource> AddJaeger(
        this IDistributedApplicationBuilder builder)
    {
        return builder.AddContainer(InfrastructureConstants.JaegerName, "jaegertracing/all-in-one:1.57")
            .WithEndpoint(port: 16686, targetPort: 16686, name: "jaeger-ui") // Jaeger UI
            .WithEndpoint(port: InfrastructureConstants.JaegerPort, targetPort: InfrastructureConstants.JaegerPort, name: "jaeger-agent"); // Jaeger agent
            //.WithEnvironment("COLLECTOR_OTLP_ENABLED", "true"); // Enable OTLP receiver
    }
}
