using Bfs.Core.ObjectFields;

namespace Bfs.Master.Data
{
    public class BfsComponentListItem
    {      
        public string? Id { get; set; }
public string? BfsSystemId { get; set; }
public bool? IsSoftDelete { get; set; }
public string? Name { get; set; }
public string? DisplayName { get; set; }
public string? DataTypeId { get; set; }
public string? MenuName { get; set; }
public string? MenuPlaceHolder { get; set; }
public string? QueryBaseTable { get; set; }
public string? Notes { get; set; }
public string? InterfaceRequired { get; set; }

        public string? BfsSystemName { get; set; }
public string? DataTypeName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}

