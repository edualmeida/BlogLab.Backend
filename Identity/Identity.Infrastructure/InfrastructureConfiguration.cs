﻿using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddIdentityInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
        => services
            .AddIdentity()
            .AddDabaseStorage<IdentityDbContext>(
                configuration,
                Assembly.GetExecutingAssembly())
            .AddTransient<IDbInitializer, IdentityDbInitializer>()
            .AddTransient<IIdentityQueryRepository, IdentityRepository>();

    private static IServiceCollection AddIdentity(
        this IServiceCollection services)
    {
        services
            .AddTransient<IIdentity, IdentityService>()
            .AddTransient<IJwtGenerator, JwtGeneratorService>()
            .AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = UserModelConstants.Identity.MinPasswordLength;
            })
            .AddEntityFrameworkStores<IdentityDbContext>();

        return services;
    }
}