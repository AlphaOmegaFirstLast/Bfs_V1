namespace Bfs.Core.ObjectFields;

public class FieldValidation
{
    ///<Summary>
    /// TableField Required.
    ///</Summary>
    public bool? IsRequired { get; set; } = false;

    ///<Summary>
    /// TableField Min Length.
    ///</Summary>
    public string? MinLength { get; set; } = string.Empty;
    ///<Summary>
    /// TableField Max Length.
    ///</Summary>
    public string? MaxLength { get; set; } = string.Empty;

    ///<Summary>
    /// TableField Min Value.
    ///</Summary>
    public string? MinValue { get; set; } = string.Empty;
    ///<Summary>
    /// TableField Max Value.
    ///</Summary>
    public string? MaxValue { get; set; } = string.Empty;

    ///<Summary>
    /// RegexPattern for validating the TableField.
    ///</Summary>
    public string? RegexPattern { get; set; } = string.Empty;

    ///<Summary>
    /// Semi-Colon separated values that are allowed for the TableField.
    ///</Summary>
    public string? AllowedValues { get; set; } = string.Empty;

}