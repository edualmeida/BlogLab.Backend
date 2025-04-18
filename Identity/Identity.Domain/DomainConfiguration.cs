using System.Reflection;
using Common.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Domain;
public static class DomainConfiguration
{
    public static IServiceCollection AddIdentityDomain(
        this IServiceCollection services)
        => services
            .AddCommonDomain(
                Assembly.GetExecutingAssembly());
}