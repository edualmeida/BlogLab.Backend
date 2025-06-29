
using Common.Infrastructure;
using Common.Web;
using Identity.Application;
using Identity.Domain;
using Identity.Infrastructure;
using Identity.Infrastructure.Persistence;

namespace Identity.Api;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        builder.Services
            .AddIdentityDomain()
            .AddIdentityApplication(builder.Configuration)
            .AddIdentityInfrastructure(builder.Configuration)
            .AddIdentityWebComponents();

        builder.AddHostingInfrastructure<AppIdentityDbContext>();

        var app = builder.Build();

        app.UseCommonWebComponents();

        app.Run();
    }
}
