using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

public static class WebConfiguration
{
    public static IServiceCollection AddWebComponents(this IServiceCollection services,
        Type applicationConfigurationType)
    {
        services
            .AddValidatorsFromAssemblyContaining(applicationConfigurationType)
            .AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters()
            .AddScoped<ICurrentUser, CurrentUserService>();

        return services;
    }

    public static IServiceCollection AddModelBinders(
       this IServiceCollection services)
    {
        services
            .AddControllers();

        return services;
    }
}