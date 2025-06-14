
using Identity.Application;
using Identity.Infrastructure;
using Identity.Domain;
using Scalar.AspNetCore;

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

        var app = builder.Build();

        app.MapDefaultEndpoints();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
