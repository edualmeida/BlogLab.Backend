using System.Reflection;
using Identity.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Web;
public static class WebConfiguration
{
    public static IServiceCollection AddIdentityWebComponents(
        this IServiceCollection services)
        => services.AddWebComponents(
            typeof(IdentityApplicationConfiguration), Assembly.GetExecutingAssembly());
}