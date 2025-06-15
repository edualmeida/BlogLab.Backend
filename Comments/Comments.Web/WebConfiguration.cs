using System.Reflection;
using Comments.Application;
using Common.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Scalar.AspNetCore;

namespace Comments.Web;
public static class WebConfiguration
{
    public static IServiceCollection AddCommentsWebComponents(
        this IServiceCollection services)
        => services.AddWebComponents(typeof(CommentsApplicationConfiguration), Assembly.GetExecutingAssembly());

    
}