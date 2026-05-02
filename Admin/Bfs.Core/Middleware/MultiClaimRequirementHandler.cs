using Bfs.Core.Auth;
using Bfs.Core.Config;
using Bfs.Core.Interfaces;
using Bfs.Core.TenantManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Bfs.Core.Middleware;

public class MultiClaimRequirementHandler: AuthorizationHandler<MultiClaimRequirement>
{
    private readonly BfsSettings _settings;
    private readonly IScopeData _scopeData;
    private readonly ITenantManager _tenantManager;
    private readonly IPermissionProvider _permissionProvider;

    public MultiClaimRequirementHandler(
        IOptions<BfsSettings> settings,
        IScopeData scopeData,
        ITenantManager tenantManager,
        IPermissionProvider permissionProvider)
    {
        _settings = settings.Value;
        _scopeData = scopeData;
        _tenantManager = tenantManager;
        _permissionProvider = permissionProvider;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MultiClaimRequirement requirement)
    {
        if (!_settings.IsSecurityEnabled)
        {
            context.Succeed(requirement);
            return; // Security is disabled, so we succeed the requirement
        }

        //context.User?.Identity?.IsAuthenticated == true, that means the jwt token is valid and the user is authenticated.
        if (context.User?.Identity?.IsAuthenticated != true) 
            return; // User is not authenticated, so we don't succeed the requirement

        var userRoles = context.User.Claims.Where(x=> x.Type == "roleId").Select(c => c.Value).ToList();
        if (userRoles.Contains(BfsDefault.BfsAdminRoleId) || userRoles.Contains(BfsDefault.IdentityRoleId))
        {
            context.Succeed(requirement);
            return;
        }

        // get tenant-specific permissions, get user 's roles, and check if any of the user's roles have the required permissions
        var tenantId = _scopeData.TenantId; //_tenantManager.GetCurrentTenantId();
        var tenantDb = _tenantManager.GetTenantDbConnection();
        var masterConnection = _settings.DbConnections.MasterConnection;

        var permissions = await _permissionProvider.GetPermissionsAsync(tenantId.ToString(), masterConnection, tenantDb);
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
