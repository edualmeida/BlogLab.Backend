using System.Reflection;
using Comments.Application;
using Common.Web;
using Microsoft.Extensions.DependencyInjection;

namespace Comments.Web;
public static class WebConfiguration
{
    public static IServiceCollection AddCommentsWebComponents(
        this IServiceCollection services)
        => services.AddWebComponents(typeof(CommentsApplicationConfiguration), Assembly.GetExecutingAssembly());
}