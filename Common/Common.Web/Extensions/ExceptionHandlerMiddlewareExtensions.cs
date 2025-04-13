using Common.Web.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Common.Web.Extensions;
public static class ExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder SetupExceptionHandling(
        this IApplicationBuilder builder)
        => builder.UseMiddleware<ExceptionHandlerMiddleware>();
}
