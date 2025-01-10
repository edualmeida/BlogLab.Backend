using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

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

    public static IServiceCollection AddTokenAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var configKey = configuration
            .GetSection(nameof(ApplicationSettings))
            .GetValue<string>(nameof(ApplicationSettings.JwtPrivateKey));

        var privateKey = Encoding.UTF8.GetBytes(configKey!);

        services
            .AddAuthentication(authentication =>
            {
                authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(bearer =>
            {
                bearer.RequireHttpsMetadata = false;
                bearer.SaveToken = true;
                bearer.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(privateKey),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            })
            .AddScheme<ApiKeySchemeOptions, ApiKeyHandler>(ApiKey.SchemeName, options =>
            {
                configuration.GetRequiredSection("ApiKeyOptions").Bind(options);
            });

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
                .UseSqlite(configuration.GetDefaultConnectionString(), sqlOptions => sqlOptions
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