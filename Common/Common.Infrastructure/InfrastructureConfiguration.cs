using Common.Application.Contracts;
using Common.Domain;
using Common.Domain.Telemetry;
using Common.Infrastructure.Authentication.HttpMessageHandlers;
using Common.Infrastructure.Extensions;
using Common.Infrastructure.Telemetry;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Reflection;

namespace Common.Infrastructure;
public static class InfrastructureConfiguration
{
    public static void AddHostingInfrastructure<TDbContext>(
        this WebApplicationBuilder builder)
        where TDbContext : DbContext
    {
        builder.AddRedisClient(connectionName: InfrastructureConstants.RedisCacheName);
        builder.EnrichNpgsqlDbContext<TDbContext>();
    }

    public static IServiceCollection AddCommonInfrastructure(
        this IServiceCollection services,
        Assembly assembly,
        IConfiguration configuration)
    {
        services
            //.AddOpenTelemetry(configuration)
            .AddTelemetryWorkers(assembly)
            .AddAuthenticationHandlers(configuration)
            .AddSingleton<IActivityScopeFactory, ActivityScopeFactory>();

        return services;
    }

    public static IServiceCollection AddDatabaseStorage<TDbContext>(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly assembly)
        where TDbContext : DbContext
        => services
            .AddDatabase<TDbContext>(configuration, InfrastructureConstants.BlogLabDatabaseName)
            .AddRepositories(assembly);

    public static IServiceCollection AddAuthenticationHandlers(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddAuthentication(authentication =>
            {
                authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtAuthenticationScheme(configuration)
            .AddApiKeyAuthenticationScheme(configuration);

        services.AddHttpContextAccessor();

        services.AddTransient<FowardAuthorizationHeaderHandler>();

        return services;
    }
    public static void ConfigureApiKey(
    this HttpClient httpClient,
    IConfiguration configuration)
    {
        var options = configuration.GetApiKeySchemeOptions();
        httpClient.DefaultRequestHeaders
                .Add(options.HeaderName, options.ApiKey);
    }

    public static IHttpClientBuilder ConfigureDefaultHttpClientHandler(this IHttpClientBuilder builder)
        => builder
            .ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(5)
            })
            .SetHandlerLifetime(Timeout.InfiniteTimeSpan);

    public static IServiceCollection AddRepositories(
        this IServiceCollection services,
        Assembly assembly)
        => services
            .Scan(scan => scan
                .FromAssemblies(assembly)
                .AddClasses(classes => classes
                    .AssignableTo(typeof(IDomainRepository<>))
                    .AssignableTo(typeof(IQueryRepository<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

    public static IServiceCollection AddDatabase<TDbContext>(
        this IServiceCollection services,
        IConfiguration configuration,
        string connectionStringName)
        where TDbContext : DbContext
        => services
            .AddScoped<DbContext, TDbContext>()
            .AddDbContext<TDbContext>(options => options
                .UseNpgsql(
                    configuration.GetConnectionString(connectionStringName)!, 
                    sqlOptions => sqlOptions.MigrationsAssembly(typeof(TDbContext).Assembly.FullName)
                )
                .UseSnakeCaseNamingConvention()
            );

    private static IServiceCollection AddTelemetryWorkers(
        this IServiceCollection services,
        Assembly assembly)
        => services
            .Scan(scan => scan
                .FromAssemblies(assembly)
                .AddClasses(classes => classes.AssignableTo(typeof(ITelemetry)))
                .AsImplementedInterfaces()
                .WithSingletonLifetime());

    public static IServiceCollection AddOpenTelemetry(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var tracingOtlpEndpoint = configuration["OTLP_ENDPOINT_URL"];
        var otel = services.AddOpenTelemetry();

        string HealthEndpointPath = "/health";
        string AlivenessEndpointPath = "/alive";

        // Configure OpenTelemetry Resources with the application name
        otel.ConfigureResource(resource => resource
            .AddService(serviceName: "bloglab"));

        // Add Metrics for ASP.NET Core and our custom metrics and export to Prometheus
        otel.WithMetrics(metrics => metrics
            // Metrics provider from OpenTelemetry
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddMeter("ArticleCatalog")
            // Metrics provides by ASP.NET Core in .NET 8
            .AddMeter("Microsoft.AspNetCore.Hosting")
            .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
            // Metrics provided by System.Net libraries
            .AddMeter("System.Net.Http")
            .AddMeter("System.Net.NameResolution")
            .AddPrometheusExporter());

        // Add Tracing for ASP.NET Core and our custom ActivitySource and export to Jaeger
        otel.WithTracing(tracing =>
        {
            tracing.AddAspNetCoreInstrumentation(tracing =>
                        // Don't trace requests to the health endpoint to avoid filling the dashboard with noise
                tracing.Filter = httpContext =>
                    !(httpContext.Request.Path.StartsWithSegments(HealthEndpointPath)
                      || httpContext.Request.Path.StartsWithSegments(AlivenessEndpointPath))
            );
            tracing.AddHttpClientInstrumentation();
            tracing.AddSource("OtPrGrYa.Example");
            if (tracingOtlpEndpoint != null)
            {
                tracing.AddOtlpExporter(otlpOptions =>
                {
                    otlpOptions.Endpoint = new Uri(tracingOtlpEndpoint);
                });
            }
            else
            {
                tracing.AddConsoleExporter();
            }
        });

        return services;
    }
}