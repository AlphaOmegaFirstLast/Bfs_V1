namespace Bfs.Core.ObjectFields;

public class CustomField
{
    ///<Summary>
    ///Custom Field Definition Id ref
    ///</Summary>
    public long? CustomFieldDefinitionId { get; set; } = 0;
    ///<Summary>
    /// CustomField DisplayName.
    ///</Summary>
    public string? Name { get; set; } = string.Empty;
    ///<Summary>
    /// CustomField Value.
    ///</Summary>
    public string? Value { get; set; } = string.Empty;
}