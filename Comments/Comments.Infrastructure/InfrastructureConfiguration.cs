using Comments.Application.Services;
using Comments.Infrastructure.Extensions;
using Comments.Infrastructure.HttpServices;
using Common.Infrastructure;
using Common.Infrastructure.Repositories.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using System.Reflection;

namespace Comments.Infrastructure;
public static class InfrastructureConfiguration
{
    public static IServiceCollection AddCommentsInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

        return services
            .Configure<MongoStoreDatabaseSettings>(configuration.GetSection("CommentsStoreDatabase"))
            .AddRepositories(Assembly.GetExecutingAssembly())
            .AddHttpClients(configuration);
    }

    private static IServiceCollection AddHttpClients(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var httpClientSettings = configuration.GetCommentsSettings();
        services.AddHttpClient<AuthorsHttpService>("CommentsAuthorsHttpService", httpClient =>
            {
                httpClient.BaseAddress = new Uri(httpClientSettings.AuthorsApiClientSettings.BaseUrl);
                httpClient.ConfigureApiKey(configuration);
            })
            .ConfigureDefaultHttpClientHandler()
            .AddTypedClient<IAuthorsHttpService, AuthorsHttpService>();
        
        return services;
    }
}