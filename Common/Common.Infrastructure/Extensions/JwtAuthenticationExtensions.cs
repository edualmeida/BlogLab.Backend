using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Common.Infrastructure.Extensions;

internal static class JwtAuthenticationExtensions
{
    public static AuthenticationBuilder AddJwtAuthenticationScheme(
        this AuthenticationBuilder authentication,
        IConfiguration configuration)
    {
        var configKey = configuration
            .GetSection(nameof(ApplicationSettings))
            .GetValue<string>(nameof(ApplicationSettings.JwtPrivateKey));

        var privateKey = Encoding.UTF8.GetBytes(configKey!);

        authentication.AddJwtBearer(bearer =>
        {
            bearer.RequireHttpsMetadata = false;
            bearer.SaveToken = true;
            bearer.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(privateKey),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
        return authentication;
    }
}