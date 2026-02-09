using Bfs.Core.ObjectFields;

namespace Bfs.Infrastructure.Data
{
    public class BfsComponentSystemActionListItem
    {      
        public string? BfsComponentSystemActionId { get; set; }
public string? BfsComponentSystemActionBfsComponentId { get; set; }
public string? BfsComponentSystemActionSystemActionId { get; set; }
public string? BfsComponentSystemActionActionLocationId { get; set; }

        public string? BfsComponentName { get; set; }
public string? SystemActionName { get; set; }
public string? ActionLocationName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}