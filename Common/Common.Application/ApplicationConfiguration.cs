using System.Reflection;
using Common.Application.Behaviors;
using Common.Application.Mapping;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Application;
public static class ApplicationConfiguration
{
    public static IServiceCollection AddCommonApplication(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly assembly)
        => services
            .Configure<ApplicationSettings>(
                configuration.GetSection(nameof(ApplicationSettings)),
                options => options.BindNonPublicProperties = true)
            .AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(assembly);
                cfg.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            })
            .AddAutoMapperProfile(assembly);

    private static IServiceCollection AddAutoMapperProfile(
        this IServiceCollection services, Assembly assembly)
        => services
            .AddAutoMapper(
                (_, config) => config
                    .AddProfile(new MappingProfile(assembly)), Array.Empty<Assembly>());
}