using Bfs.Core.ObjectFields;

namespace Bfs.Master.Contracts
{
    public class BfsFieldListItem
    {      
        public long Id { get; set; }
public long BfsComponentId { get; set; }
public string Field { get; set; }
public string DisplayName { get; set; }
public int FilterTypeId { get; set; }
public int BackendDataTypeId { get; set; }

        public FieldValidation FieldValidation { get; set; }
        public string? JsonFieldValidation { get; set; }
public ReportInfo ReportInfo { get; set; }
        public string? JsonReportInfo { get; set; }
public MatrixInfo MatrixInfo { get; set; }
        public string? JsonMatrixInfo { get; set; }
public ToolTipInfo ToolTipInfo { get; set; }
        public string? JsonToolTipInfo { get; set; }
public FormInfo FormInfo { get; set; }
        public string? JsonFormInfo { get; set; }

        public string? FilterTypeName { get; set; }
public string? BackendDataTypeName { get; set; }

        public string? BfsComponentName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}

