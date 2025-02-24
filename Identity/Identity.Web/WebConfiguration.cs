using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

public static class WebConfiguration
{
    public static IServiceCollection AddIdentityWebComponents(
        this IServiceCollection services)
        => services.AddWebComponents(
            typeof(IdentityApplicationConfiguration), Assembly.GetExecutingAssembly());
}