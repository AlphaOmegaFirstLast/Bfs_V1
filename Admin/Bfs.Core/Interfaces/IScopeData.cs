namespace Bfs.Core.Interfaces;

/// <summary>
///     Data, related to the scope of a certain operation.
/// </summary>
public interface IScopeData
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
    public string CorrelationToken { get; }

    /// <summary>
    ///     The source of the request. Portal \ Api \ Bulk Import \ Data Migratiom
    /// </summary>
    public string RequestSource { get; }
}