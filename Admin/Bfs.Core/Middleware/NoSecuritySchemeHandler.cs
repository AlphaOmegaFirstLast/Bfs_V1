using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Bfs.Core.Middleware;

public class NoSecuritySchemeOptions : AuthenticationSchemeOptions
{
}

public class NoSecuritySchemeHandler : AuthenticationHandler<NoSecuritySchemeOptions>
{
    // Required constructor
    public NoSecuritySchemeHandler(IOptionsMonitor<NoSecuritySchemeOptions> options, ILoggerFactory logger,
        UrlEncoder encoder)
        : base(options, logger, encoder)
    {
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // 1. Get token from a custom header
        // 2. Validation logic (e.g., look up user by token)
        // 3. Create the user identity
        var claims = new[] { new Claim(ClaimTypes.Name, "NoSecurityUser"), new Claim(ClaimTypes.Role, "bfs.admin") };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        // 4. Success
        return AuthenticateResult.Success(ticket);
    }
}