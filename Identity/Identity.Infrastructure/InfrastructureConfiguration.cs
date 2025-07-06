using System.Reflection;
using Common.Infrastructure;
using Identity.Application;
using Identity.Application.Queries;
using Identity.Domain.Models.Users;
using Identity.Infrastructure.Persistence;
using Identity.Infrastructure.Repositories;
using Identity.Infrastructure.Services;
using Identity.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure;
public static class InfrastructureConfiguration
{
    public static IServiceCollection AddIdentityInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
        => services
            .AddCommonInfrastructure(Assembly.GetExecutingAssembly(), configuration)
            .AddTransient<IIdentity, IdentityService>()
            .AddIdentityStores()
            .AddDatabaseStorage<AppIdentityDbContext>(
                configuration,
                Assembly.GetExecutingAssembly())
            .AddTransient<IIdentityQueryRepository, IdentityRepository>();

    public static IServiceCollection AddIdentityStores(
        this IServiceCollection services)
    {
        services
            .AddTransient<IJwtGenerator, JwtGeneratorService>()
            .AddIdentity<User, Role>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = UserModelConstants.Identity.MinPasswordLength;
            })
            .AddEntityFrameworkStores<AppIdentityDbContext>();

        return services;
    }
}