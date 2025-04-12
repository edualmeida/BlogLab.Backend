using System.Reflection;
using Common.Application.Contracts;
using Common.Web.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Web;
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
            .AddScoped<ICurrentUserService, CurrentUserService>()
            .AddAutoMapperProfile(assembly);

        return services;
    }

    public static IServiceCollection AddModelBinders(
       this IServiceCollection services)
    {
        services
            .AddControllers()
            // .AddJsonOptions(options =>
            // {
            //     options.JsonSerializerOptions.Converters.Add(new StringToGuidConverter());
            // })
            ;

        return services;
    }
    
    private static IServiceCollection AddAutoMapperProfile(
        this IServiceCollection services, Assembly assembly)
        => services
            .AddAutoMapper((_, config) => config
                .AddProfile(new MappingProfile(assembly)), Array.Empty<Assembly>());
}