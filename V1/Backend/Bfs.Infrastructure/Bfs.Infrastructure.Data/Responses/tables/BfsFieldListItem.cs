using Bfs.Core.ObjectFields;

namespace Bfs.Infrastructure.Data
{
    public class BfsFieldListItem
    {      
        public string? BfsFieldFieldValidation { get; set; }
public string? BfsFieldId { get; set; }
public string? BfsFieldBfsComponentId { get; set; }
public string? BfsFieldField { get; set; }
public string? BfsFieldDisplayName { get; set; }
public string? BfsFieldIsQueryColumn { get; set; }
public string? BfsFieldIsJoinField { get; set; }
public string? BfsFieldParentTable { get; set; }
public string? BfsFieldFilterTypeId { get; set; }
public string? BfsFieldBackendDataTypeId { get; set; }
public string? BfsFieldReportInfo { get; set; }
public string? BfsFieldMatrixInfo { get; set; }
public string? BfsFieldToolTipInfo { get; set; }
public string? BfsFieldFormInfo { get; set; }

        public string? BfsComponentName { get; set; }
public string? FilterTypeName { get; set; }
public string? BackendDataTypeName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}