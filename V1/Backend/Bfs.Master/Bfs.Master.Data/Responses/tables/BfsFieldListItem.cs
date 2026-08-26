using Bfs.Core.ObjectFields;

namespace Bfs.Master.Data
{
    public class BfsFieldListItem
    {
        public string? Id { get; set; }
        public string? BfsComponentId { get; set; }
        public string? Field { get; set; }
        public string? DisplayName { get; set; }
        public int? FilterTypeId { get; set; }
        public int? BackendDataTypeId { get; set; }

        //object fields
        public FieldValidation? FieldValidation { get; set; }
        public string? jsonFieldValidation { get; set; }

        public ReportInfo? ReportInfo { get; set; }
        public string? jsonReportInfo { get; set; }

        public MatrixInfo? MatrixInfo { get; set; }
        public string? jsonMatrixInfo { get; set; }

        public ToolTipInfo? ToolTipInfo { get; set; }
        public string? jsonToolTipInfo { get; set; }

        public FormInfo? FormInfo { get; set; }
        public string? jsonFormInfo { get; set; }

        public string? BfsComponentName { get; set; }
        public string? FilterTypeName { get; set; }
        public string? BackendDataTypeName { get; set; }

        //manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
    }
}

