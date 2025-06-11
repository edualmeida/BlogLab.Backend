
using ArticleCatalog.Application;
using ArticleCatalog.Domain;
using ArticleCatalog.Infrastructure;
using Scalar.AspNetCore;

namespace ArticleCatalog.Api;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        builder.Services
            .AddArticleCatalogDomain()
            .AddArticleCatalogApplication(builder.Configuration)
            .AddArticleCatalogInfrastructure(builder.Configuration)
            .AddArticleCatalogWebComponents();

        var app = builder.Build();

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
