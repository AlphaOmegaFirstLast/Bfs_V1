using Bfs.Core.ObjectFields;

namespace Bfs.Master.Data
{
    public class BfsComponentSystemActionListItem
    {      
        public long Id { get; set; }
public long BfsComponentId { get; set; }
public long SystemActionId { get; set; }
public int ActionLocationId { get; set; }

        public string? BfsComponentName { get; set; }
public string? SystemActionName { get; set; }
public string? ActionLocationName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}