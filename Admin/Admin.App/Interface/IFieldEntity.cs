using Admin.App.Constants;
using Bfs.Core.ObjectFields;

namespace Admin.App
{
    public interface IFieldEntity
    {
        public long Id { get; set; }
        public long BfsComponentId { get; set; }
        public string Field { get; set; }
        public string DisplayName { get; set; }
        //public bool IsQueryColumn { get; set; }
        //public bool IsJoinField { get; set; }
        //public string ParentTable { get; set; }
        public FilterType FilterTypeId { get; set; }
        public BackendDataType BackendDataTypeId { get; set; }
        public FieldValidation FieldValidation { get; set; }
        public FormInfo FormInfo { get; set; }
        public ReportInfo ReportInfo { get; set; }
        public MatrixInfo MatrixInfo { get; set; }
        public ToolTipInfo ToolTipInfo { get; set; }
    }
}