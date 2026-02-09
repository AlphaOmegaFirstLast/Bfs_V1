using Bfs.Core.ObjectFields;

namespace Bfs.Infrastructure.Data
{
    public class BfsComponentBusinessActionListItem
    {      
        public string? BfsComponentBusinessActionId { get; set; }
public string? BfsComponentBusinessActionBfsComponentId { get; set; }
public string? BfsComponentBusinessActionBusinessActionId { get; set; }
public string? BfsComponentBusinessActionActionLocationId { get; set; }

        public string? BfsComponentName { get; set; }
public string? BusinessActionName { get; set; }
public string? ActionLocationName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}