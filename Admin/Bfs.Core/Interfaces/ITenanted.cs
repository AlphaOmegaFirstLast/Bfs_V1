namespace Bfs.Core.Interfaces;

/// <summary>
///     Tenanted entity, belonging solely to one tenant.
/// </summary>
public interface ITenanted
{
    /// <summary>
    ///     ID of the tenants who owns the entity.
    /// </summary>
    long TenantId { get; set; }
}