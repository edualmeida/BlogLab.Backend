using System.Reflection;
using Common.Web;
using Identity.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Api;
public static class WebConfiguration
{
    public static IServiceCollection AddIdentityWebComponents(
        this IServiceCollection services)
        => services.AddWebComponents(
            typeof(IdentityApplicationConfiguration), Assembly.GetExecutingAssembly());
}