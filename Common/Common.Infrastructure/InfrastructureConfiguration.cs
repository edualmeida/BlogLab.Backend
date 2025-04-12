using System.Reflection;
using Common.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure;
public static class InfrastructureConfiguration
{
    public static IServiceCollection AddDabaseStorage<TDbContext>(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly assembly)
        where TDbContext : DbContext
        => services
            .AddDatabase<TDbContext>(configuration)
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

    private static IServiceCollection AddDatabase<TDbContext>(
        this IServiceCollection services,
        IConfiguration configuration)
        where TDbContext : DbContext
        => services
            .AddScoped<DbContext, TDbContext>()
            .AddDbContext<TDbContext>(options => options
                .UseNpgsql(configuration.GetConnectionString("DefaultConnection")!, sqlOptions => sqlOptions
                        .MigrationsAssembly(typeof(TDbContext).Assembly.FullName)));

    internal static IServiceCollection AddRepositories(
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
}