namespace Bfs.Core.Data;

internal class DataError
{
    /// <summary>
    ///     Error message for retrieving an out-of-scope entity.
    ///     This is internal error for safe-guarding development. This error should not be exposed publicly.
    /// </summary>
    public static readonly string GetOutOfScope =
        "You are trying to get an entity outside of the scope of the current tenant";

    /// <summary>
    ///     Error message for creating an out-of-scope entity.
    ///     This is internal error for safe-guarding development. This error should not be exposed publicly.
    /// </summary>
    public static readonly string CreateOutOfScope =
        "You are trying to create an entity outside of the scope of the current tenant";

    /// <summary>
    ///     Error message for updating an out-of-scope entity.
    ///     This is internal error for safe-guarding development. This error should not be exposed publicly.
    /// </summary>
    public static readonly string UpdateOutOfScope =
        "You are trying to update an entity outside of the scope of the current tenant";

    /// <summary>
    ///     Error message for deleting an out-of-scope entity.
    ///     This is internal error for safe-guarding development. This error should not be exposed publicly.
    /// </summary>
    public static readonly string DeleteOutOfScope =
        "You are trying to delete an entity outside of the scope of the current tenant";

    /// <summary>
    ///     Error message for saving an out-of-scope entity.
    ///     This is internal error for safe-guarding development. This error should not be exposed publicly.
    /// </summary>
    public static readonly string SaveOutOfScope =
        "You are trying to save an entity outside of the scope of the current tenant";
}