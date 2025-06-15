using Comments.Infrastructure;
using Comments.Domain;
using Comments.Application;
using Common.Web;

namespace Comments.Api;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        builder.Services
            .AddCommentsDomain()
            .AddCommentsApplication(builder.Configuration)
            .AddCommentsInfrastructure(builder.Configuration)
            .AddCommentsWebComponents();

        var app = builder.Build();

        app.UseCommonWebComponents();

        app.Run();
    }
}
