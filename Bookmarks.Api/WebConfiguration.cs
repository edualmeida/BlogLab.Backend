using System.Reflection;
using Bookmarks.Application;
using Common.Web;
using Microsoft.Extensions.DependencyInjection;

namespace Bookmarks.Api;
public static class WebConfiguration
{
    public static IServiceCollection AddBookmarksWebComponents(
        this IServiceCollection services)
        => services.AddCommonWebComponents(typeof(BookmarksApplicationConfiguration), Assembly.GetExecutingAssembly());
}