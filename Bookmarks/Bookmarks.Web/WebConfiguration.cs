using System.Reflection;
using Bookmarks.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Bookmarks.Web;
public static class WebConfiguration
{
    public static IServiceCollection AddBookmarksWebComponents(
        this IServiceCollection services)
        => services.AddWebComponents(typeof(BookmarksApplicationConfiguration), Assembly.GetExecutingAssembly());
}