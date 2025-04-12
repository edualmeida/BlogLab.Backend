using System.Reflection;
using Common.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Application;
public static class IdentityApplicationConfiguration
{
    public static IServiceCollection AddIdentityApplication(
        this IServiceCollection services,
        IConfiguration configuration)
        => services
            .AddCommonApplication(
                configuration, 
                Assembly.GetExecutingAssembly());
}