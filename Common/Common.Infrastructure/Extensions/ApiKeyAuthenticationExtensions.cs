using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;

namespace Common.Infrastructure.Extensions;

internal static class ApiKeyAuthenticationExtensions
{
    public static ApiKeySchemeOptions GetApiKeySchemeOptions(
        this IConfiguration configuration)
    {
        var options = new ApiKeySchemeOptions();
        configuration.GetRequiredSection("ApiKeyOptions").Bind(options);

        return options;
    }

    public static AuthenticationBuilder AddApiKeyAuthenticationScheme(
        this AuthenticationBuilder authentication,
        IConfiguration configuration)
    {
        authentication.AddScheme<ApiKeySchemeOptions, ApiKeyHandler>(ApiKey.SchemeName, options =>
        {
            configuration.GetRequiredSection(Constants.ApiKeyOptions).Bind(options);
        });
        return authentication;
    }
}