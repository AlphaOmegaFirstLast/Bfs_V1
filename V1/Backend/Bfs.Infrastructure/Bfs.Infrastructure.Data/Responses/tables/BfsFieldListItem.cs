using Bfs.Core.ObjectFields;

namespace Bfs.Infrastructure.Data
{
    public class BfsFieldListItem
    {      
        public string? Id { get; set; }
public string? BfsComponentId { get; set; }
public string? Field { get; set; }
public string? DisplayName { get; set; }
public string? ParentTable { get; set; }
public string? FilterTypeId { get; set; }
public string? BackendDataTypeId { get; set; }

        public string? BfsComponentName { get; set; }
public string? FilterTypeName { get; set; }
public string? BackendDataTypeName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}