namespace Bfs.Core.ObjectFields;

public class ReportInfo 
{
    public string? ParentTable { get; set; } = string.Empty;
    public bool? IsQueryColumn { get; set; } = true;
    public bool? IsColumnVisible { get; set; } = true;
    public bool? IsJoinField { get; set; } = false;
    public int? AggregateTypeId { get; set; } = 0;
    public int? ChartElementId { get; set; } = 0; 
    public int? ColumnOrder { get; set; } = 1;
}