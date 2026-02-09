using Bfs.Core.ObjectFields;

namespace Bfs.Infrastructure.Contracts
{
    public class BfsComponentListItem
    {      
        public string? BfsComponentId { get; set; }
public string? BfsComponentBfsSystemId { get; set; }
public string? BfsComponentIsSoftDelete { get; set; }
public string? BfsComponentName { get; set; }
public string? BfsComponentDisplayName { get; set; }
public string? BfsComponentDataTypeId { get; set; }
public string? BfsComponentMenuName { get; set; }
public string? BfsComponentMenuPlaceHolder { get; set; }
public string? BfsComponentQueryBaseTable { get; set; }
public string? BfsComponentNotes { get; set; }

        public string? BfsSystemName { get; set; }
public string? DataTypeName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}