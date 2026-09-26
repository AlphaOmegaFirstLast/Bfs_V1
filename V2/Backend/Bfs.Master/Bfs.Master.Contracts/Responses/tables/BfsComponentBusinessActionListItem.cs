using Bfs.Core.ObjectFields;

namespace Bfs.Master.Contracts
{
    public class BfsComponentBusinessActionListItem
    {      
        public long Id { get; set; }
public long BfsComponentId { get; set; }
public long BusinessActionId { get; set; }
public int ActionLocationId { get; set; }

        public string? BfsComponentName { get; set; }
public string? BusinessActionName { get; set; }
public string? ActionLocationName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}