using Bfs.Core.Interfaces;

namespace Bfs.Core.Contracts;

/// <summary>
///     Data, related to the scope of a certain operation.
/// </summary>
public class ScopeData : IScopeData
{
    /// <summary>
    ///     Name of the header containing the user ID.
    /// </summary>
    public static readonly string PortalUserIdHeader = "PortalUserId";

    /// <summary>
    ///     Name of the header containing the tenant ID.
    /// </summary>
    public static readonly string TenantIdHeader = "TenantId";

    /// <summary>
    ///     Name of the header containing the correlation token.
    /// </summary>
    public static readonly string CorrelationTokenHeader = "CorrelationToken";

    /// <summary>
    ///     ID of the user, requesting the operation.
    /// </summary>
    public long PortalUserId { get; }

    /// <summary>
    ///     ID of the tenant, owning the entities in the scope.
    /// </summary>
    public long TenantId { get; }

    /// <summary>
    ///     Correlation token for sequence chaining.
    /// </summary>
    public string CorrelationToken { get; } // Changed from string? to string to match IScopeData interface

    /// <summary>
    ///     The source of the request. Portal \ Api \ Bulk Import \ Data Migration
    /// </summary>
    public string RequestSource { get; } // Changed from string? to string to match IScopeData interface
}