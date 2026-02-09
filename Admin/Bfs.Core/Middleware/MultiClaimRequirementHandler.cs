using Bfs.Core.Config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Bfs.Core.Middleware;

public class MultiClaimRequirementHandler : AuthorizationHandler<MultiClaimRequirement>
{
    private readonly BfsSettings _settings;

    public MultiClaimRequirementHandler(IOptions<BfsSettings> settings)
    {
        _settings = settings.Value;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        MultiClaimRequirement requirement)
    {
        if (!_settings.IsSecurityEnabled) context.Succeed(requirement);

        var allMatch = true;

        foreach (var kvp in requirement.RequiredClaims)
        {
            var hasClaim = context.User.HasClaim(c =>
                c.Type == kvp.Key && c.Value == kvp.Value);

            if (!hasClaim)
            {
                allMatch = false;
                break;
            }
        }

        if (allMatch) context.Succeed(requirement);

        return Task.CompletedTask;
    }
}