using Bfs.Core.Auth;
using Bfs.Core.Config;
using Bfs.Core.TenantManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Bfs.Core.Middleware;

public class MultiClaimRequirementHandler: AuthorizationHandler<MultiClaimRequirement>
{
    private readonly BfsSettings _settings;
    private readonly ITenantProvider _tenantProvider;
    private readonly IPermissionProvider _permissionProvider;

    public MultiClaimRequirementHandler(
        IOptions<BfsSettings> settings,
        ITenantProvider tenantProvider,
        IPermissionProvider permissionProvider)
    {
        _settings = settings.Value;
        _tenantProvider = tenantProvider;
        _permissionProvider = permissionProvider;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MultiClaimRequirement requirement)
    {
        if (!_settings.IsSecurityEnabled)
        {
            context.Succeed(requirement);
            return; // Security is disabled, so we succeed the requirement
        }

        if (context.User?.Identity?.IsAuthenticated != true)
            return; // User is not authenticated, so we don't succeed the requirement

        var userRoles = context.User.Claims.Where(x=> x.Type == "roleId").Select(c => c.Value).ToList();
        var bfsAdmin = "1"; // Todo Assuming "1" is the role ID for bfsAdmin, this should ideally come from a config or constant
        if (userRoles.Contains(bfsAdmin))
        {
            context.Succeed(requirement);
            return;
        }

        // get tenant-specific permissions, get user 's roles, and check if any of the user's roles have the required permissions
        var tenantId = _tenantProvider.GetCurrentTenantId();
        var tenantDb = _tenantProvider.GetCurrentTenantDbConnection();
        var masterConnection = _settings.DbConnections.MasterConnection;

        var permissions = await _permissionProvider.GetPermissionsAsync(tenantId, masterConnection, tenantDb);
        var userPermissions = permissions.Where(p => userRoles.Contains(p.RoleId.ToString())).Select(p => p.Method.ToLower()).ToList();

        var isAllowed = true;
        // Check if the user has all the required claims for a specific method. If any required claim is missing, the user is not authorized.
        foreach (var requiredClaim in requirement.RequiredClaims)
        {
            var hasPermission = userPermissions.Contains(requiredClaim.Value.ToLower()); 
            if (!hasPermission)
            {
                isAllowed = false;
                break;
            }
        }

        if (isAllowed)
            context.Succeed(requirement);
    }
}
