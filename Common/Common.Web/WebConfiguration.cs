﻿using System.Diagnostics;
using System.Reflection;
using Common.Application.Contracts;
using Common.Web.Middleware;
using Common.Web.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http.Features;
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