namespace Bfs.Core.Interfaces;

/// <summary>
///     Identifiable entity with unique identifier across the whole platform
/// </summary>
public interface IIdentifiable
{
    /// <summary>
    ///     Unique entity identifier for the whole platform
    /// </summary>
    long Id { get; set; }
}