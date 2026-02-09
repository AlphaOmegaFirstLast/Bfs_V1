using Bfs.Core.ObjectFields;

namespace Bfs.BestFit.Data
{
    public class TableFieldListItem
    {
        public bool IsDeleted { get; set; }
public long Id { get; set; }
public string Field { get; set; }
public string DisplayName { get; set; }
public bool IsQueryColumn { get; set; }
public bool IsJoinField { get; set; }
public string ParentTable { get; set; }
public int UiFormControlOrder { get; set; }

        public string? Component { get; set; }
public string? FilterType { get; set; }
public string? BackendDataType { get; set; }
public string? FormControlType { get; set; }

        public string FieldValidation { get; set; }
public string ReportInfo { get; set; }
public string MatrixInfo { get; set; }
public string ToolTipInfo { get; set; }
public string FormInfo { get; set; }

        //manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
    }
}