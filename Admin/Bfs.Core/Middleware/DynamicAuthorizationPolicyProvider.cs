using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Bfs.Core.Middleware;

public class DynamicAuthorizationPolicyProvider : IAuthorizationPolicyProvider
{
    private readonly DefaultAuthorizationPolicyProvider _fallbackPolicyProvider;

    public DynamicAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
    {
        _fallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
    }

    public Task<AuthorizationPolicy?> GetDefaultPolicyAsync()
    {
        return _fallbackPolicyProvider.GetDefaultPolicyAsync();
    }

    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
    {
        return _fallbackPolicyProvider.GetFallbackPolicyAsync();
    }

    public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        if (policyName.StartsWith("DynamicPolicy:"))
        {
            var requirementText = policyName.Substring("DynamicPolicy:".Length);

            var pairs = requirementText.Split(';', StringSplitOptions.RemoveEmptyEntries);

            var dict = new Dictionary<string, string>();

            foreach (var pair in pairs)
            {
                var kv = pair.Split('=');
                if (kv.Length == 2) dict[kv[0]] = kv[1];
            }

            if (dict.Count > 0)
            {
                var policy = new AuthorizationPolicyBuilder()
                    .AddRequirements(new MultiClaimRequirement(dict))
                    .Build();

                return Task.FromResult<AuthorizationPolicy?>(policy);
            }
        }

        return _fallbackPolicyProvider.GetPolicyAsync(policyName);
    }
}