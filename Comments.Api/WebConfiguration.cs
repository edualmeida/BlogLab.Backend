using System.Reflection;
using Comments.Application;
using Common.Web;

namespace Comments.Api;
public static class WebConfiguration
{
    public static IServiceCollection AddCommentsWebComponents(
        this IServiceCollection services)
        => services.AddWebComponents(typeof(CommentsApplicationConfiguration), Assembly.GetExecutingAssembly());
}