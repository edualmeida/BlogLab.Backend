using Microsoft.Extensions.Configuration;

public static class ConfigurationExtensions
{
    public static string GetDefaultConnectionString(this IConfiguration configuration)
        => configuration.GetConnectionString("DefaultConnection");

    public static ApiKeySchemeOptions GetApiKeySchemeOptions(
    this IConfiguration configuration)
    {
        var options = new ApiKeySchemeOptions();
        configuration.GetRequiredSection("ApiKeyOptions").Bind(options);

        return options;
    }
}