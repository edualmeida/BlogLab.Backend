namespace BlogLab.AppHost.Extensions;
internal static class JaegerHostingExtensions
{
    public static IResourceBuilder<ContainerResource> AddJaeger(
        this IDistributedApplicationBuilder builder)
    {
        var jaeger = builder.AddContainer("jaeger", "jaegertracing/all-in-one:1.57")
            .WithEndpoint(port: 16686, targetPort: 16686, name: "jaeger-ui") // Jaeger UI
            .WithEndpoint(targetPort: 6831, name: "jaeger-udp", scheme: "udp") // Jaeger agent UDP
            .WithEnvironment("COLLECTOR_OTLP_ENABLED", "true"); // Enable OTLP receiver

        return jaeger;
    }
}
