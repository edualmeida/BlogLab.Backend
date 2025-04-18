using System.Reflection;
using Common.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Bookmarks.Domain;
public static class DomainConfiguration
{
    public static IServiceCollection AddBookmarksDomain(
        this IServiceCollection services)
        => services
            .AddCommonDomain(Assembly.GetExecutingAssembly());
}