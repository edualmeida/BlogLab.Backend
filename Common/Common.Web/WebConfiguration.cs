using Common.Application.Contracts;
using Common.Web.Middleware;
using Common.Web.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scalar.AspNetCore;
using System.Diagnostics;
using System.Reflection;

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
            .AddScoped<ICurrentUserService, CurrentUserService>()
            .AddAutoMapperProfile(assembly)
            .AddHttpContextAccessor();

        services.AddControllers();
        services.AddOpenApi();

        return services;
    }


    public static WebApplication UseCommonWebComponents(
        this WebApplication app)
    {
        app.MapDefaultEndpoints();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference(options => options
                .AddPreferredSecuritySchemes("X-Api-Key") // Make this the default auth method
                .AddApiKeyAuthentication("X-Api-Key", apiKey =>
                {
                    apiKey.Value = "JEXqpyi4cPGWgH";
                })
                .AddHttpAuthentication(JwtBearerDefaults.AuthenticationScheme, auth =>
                {
                    auth.Token = "";
                })
            );
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        return app;
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

    public static IServiceCollection AddExceptionHandlers(
        this IServiceCollection services)
        => services
            .AddExceptionHandler<ValidationExceptionHandler>()
            .AddExceptionHandler<GlobalExceptionHandler>()
            .AddProblemDetails(options =>
            {
                options.CustomizeProblemDetails = context =>
                {
                    context.ProblemDetails.Instance =
                        $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";

                    context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);

                    Activity? activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
                    context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
                };
            });
}