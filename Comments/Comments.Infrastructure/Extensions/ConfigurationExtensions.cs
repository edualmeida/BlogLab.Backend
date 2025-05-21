using Comments.Application.Settings;
using Microsoft.Extensions.Configuration;

namespace Comments.Infrastructure.Extensions;
public static class ConfigurationExtensions
{
    public static CommentsSettings GetCommentsSettings(
        this IConfiguration configuration)
    {
        var settings = configuration.GetSection(nameof(CommentsSettings));
        var authorsApiClientSettings = settings.GetSection(nameof(AuthorsApiClientSettings));

        return new CommentsSettings(
            new AuthorsApiClientSettings(
                authorsApiClientSettings.GetValue<string>(nameof(AuthorsApiClientSettings.BaseUrl))!)
        );
    }
}