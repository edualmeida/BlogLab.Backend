using System.Reflection;
using Comments.Application.Comments.Queries;
using Common.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Comments.Application;
public static class CommentsApplicationConfiguration
{
    private static readonly Assembly Assembly = Assembly.GetExecutingAssembly();

    public static IServiceCollection AddCommentsApplication(
        this IServiceCollection services, IConfiguration configuration)
        => services
            .AddCommonApplication(configuration, Assembly);
}