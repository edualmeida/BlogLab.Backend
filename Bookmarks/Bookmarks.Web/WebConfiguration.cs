using Microsoft.Extensions.DependencyInjection;

public static class WebConfiguration
{
    public static IServiceCollection AddBookmarksWebComponents(
        this IServiceCollection services)
        => services.AddWebComponents(typeof(BookmarksApplicationConfiguration));
}