using Bfs.Core.ObjectFields;

namespace Bfs.Master.Contracts
{
    public class BfsFieldListItem
    {      
        public string? FieldValidation { get; set; }
public string? Id { get; set; }
public string? BfsComponentId { get; set; }
public string? Field { get; set; }
public string? DisplayName { get; set; }
public string? FilterTypeId { get; set; }
public string? BackendDataTypeId { get; set; }
public string? ReportInfo { get; set; }
public string? MatrixInfo { get; set; }
public string? ToolTipInfo { get; set; }
public string? FormInfo { get; set; }

        public string? BfsComponentName { get; set; }
public string? FilterTypeName { get; set; }
public string? BackendDataTypeName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}