using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;
using System.Text.Encodings.Web;

public class ApiKeyHandler : AuthenticationHandler<ApiKeySchemeOptions>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private ILogger _logger { get; }

    public ApiKeyHandler(
        IOptionsMonitor<ApiKeySchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IHttpContextAccessor httpContextAccessor) : base(options, logger, encoder)
    {
        _logger = logger.CreateLogger(GetType().FullName!);
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        StringValues apiKeys = StringValues.Empty;

        bool apiKeyPresent = _httpContextAccessor.HttpContext?.Request.Headers.TryGetValue(Options.HeaderName, out apiKeys) ?? false;

        if (!apiKeyPresent)
        {
            return Task.FromResult(AuthenticateResult.NoResult());
        }

        if (apiKeys.Count > 1)
        {
            return Task.FromResult(AuthenticateResult.Fail("Multiple API keys found in request. Please only provide one key."));
        }

        if (string.IsNullOrEmpty(Options.ApiKey) || !Options.ApiKey.Equals(apiKeys.FirstOrDefault()))
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid API key."));
        }

        List<Claim> claims = new()
                {
                    new Claim(ClaimTypes.NameIdentifier, Options.ApiKey),
                    new Claim(ClaimTypes.Name, "API Key User")
                };

        if (Options.ReadOnly)
        {
            claims.Add(new Claim(ClaimTypes.Role, "ReadOnly"));
        }
        else
        {
            claims.Add(new Claim(ClaimTypes.Role, "ReadWrite"));
        }

        ClaimsIdentity identity = new(claims, Scheme.Name);
        ClaimsPrincipal principal = new(identity);
        AuthenticationTicket ticket = new(principal, Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
