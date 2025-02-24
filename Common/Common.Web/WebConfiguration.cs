using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

public static class WebConfiguration
{
    public static IServiceCollection AddWebComponents(
        this IServiceCollection services,
        Type applicationConfigurationType,
        Assembly assembly)
    {
        services
            .AddValidatorsFromAssemblyContaining(applicationConfigurationType)
            .AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters()
            .AddScoped<ICurrentUser, CurrentUserService>()
            .AddAutoMapperProfile(assembly);

        return services;
    }

    public static IServiceCollection AddModelBinders(
       this IServiceCollection services)
    {
        services
            .AddControllers();

        return services;
    }
    
    private static IServiceCollection AddAutoMapperProfile(
        this IServiceCollection services, Assembly assembly)
        => services
            .AddAutoMapper((_, config) => config
                .AddProfile(new MappingProfile(assembly)), Array.Empty<Assembly>());
}