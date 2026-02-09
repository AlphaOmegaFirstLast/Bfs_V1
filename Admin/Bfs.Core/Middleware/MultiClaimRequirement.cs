using Microsoft.AspNetCore.Authorization;

namespace Bfs.Core.Middleware;

public class MultiClaimRequirement : IAuthorizationRequirement
{
    public MultiClaimRequirement(Dictionary<string, string> requiredClaims)
    {
        RequiredClaims = requiredClaims;
    }

    public IReadOnlyDictionary<string, string> RequiredClaims { get; }
}