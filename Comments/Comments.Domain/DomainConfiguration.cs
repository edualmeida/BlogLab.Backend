using System.Reflection;
using Common.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Comments.Domain;
public static class DomainConfiguration
{
    public static IServiceCollection AddCommentsDomain(
        this IServiceCollection services)
        => services
            .AddCommonDomain(Assembly.GetExecutingAssembly());
}