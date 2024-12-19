using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class BookmarksApplicationConfiguration
{
    private static readonly Assembly Assembly = Assembly.GetExecutingAssembly();

    public static IServiceCollection AddBookmarksApplication(
        this IServiceCollection services,
        IConfiguration configuration)
        => services
            .AddCommonApplication(configuration, Assembly);
}