using Bfs.Core.Config;
using Bfs.Core.Interfaces;
using Bfs.Core.TenantManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Bfs.Core.Contracts;

/// <summary>
///     Data, related to the scope of a certain operation.
/// </summary>
public class ScopeData : IScopeData
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly BfsSettings _settings;

    /// <summary>
    ///     Name of the header containing the user ID.
    /// </summary>
    public static readonly string UserIdHeader = "UserId";

    /// <summary>
    ///     Name of the header containing the tenant ID.
    /// </summary>
    public static readonly string TenantIdHeader = "TenantId";

    /// <summary>
    ///     Name of the header containing the correlation token.
    /// </summary>
    public static readonly string CorrelationTokenHeader = "CorrelationToken";

    public ScopeData(IHttpContextAccessor httpContextAccessor, IOptions<BfsSettings> settings)
    {
        _settings = settings.Value;
        _httpContextAccessor = httpContextAccessor;
        var isHttpContext = _httpContextAccessor.HttpContext != null; // In some cases, like background services, there might be no HttpContext. In such cases, we should not throw an exception, but rather set default values.

        if ((!_settings.IsIdentityWeb) && (isHttpContext && !_settings.IsMigrationEnabled))  // if migration is enabled, we are in the middle of migrating and we might have some services running without HttpContext, so we should not throw an exception in that case.
        {
            UserId = GetClaimAsLong("userId");
            TenantId = GetClaimAsLong("tenantId");
            CorrelationToken = Guid.NewGuid().ToString();
        }
    }

    private string? GetClaim(string claimType) =>
        _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == claimType)?
        .Value;

    private long GetClaimAsLong(string claimType)
    {
        //ToDo handle Exception
        var value = GetClaim(claimType);
        return long.TryParse(value, out var result)
            ? result
            : _settings.IsSecurityEnabled
            ? throw new InvalidOperationException($"Claim '{claimType}' is missing or not a valid long.")
            : 0; // If security is disabled, dont throw an exception and return 0 as default value
    }

    /// <summary>
    ///     ID of the user, requesting the operation.
    /// </summary>
    public long UserId { get; }

    /// <summary>
    ///     ID of the tenant, owning the entities in the scope.
    /// </summary>
    public long TenantId { get; }

    /// <summary>
    ///     ID of the role of the User requesting the operation.
    /// </summary>
    public long RoleId { get; }

    /// <summary>
    ///     Correlation token for sequence chaining.
    /// </summary>
    public string CorrelationToken { get; } // Changed from string? to string to match IScopeData interface

    /// <summary>
    ///     The source of the request. Portal \ Api \ Bulk Import \ Data Migration
    /// </summary>
    public string RequestSource { get; } = "Frontend"; // Changed from string? to string to match IScopeData interface
}