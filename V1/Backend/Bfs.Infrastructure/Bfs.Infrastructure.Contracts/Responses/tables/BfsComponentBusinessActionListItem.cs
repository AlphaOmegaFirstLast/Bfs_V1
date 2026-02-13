using Bfs.Core.ObjectFields;

namespace Bfs.Infrastructure.Contracts
{
    public class BfsComponentBusinessActionListItem
    {      
        public string? Id { get; set; }
public string? BfsComponentId { get; set; }
public string? BusinessActionId { get; set; }
public string? ActionLocationId { get; set; }

        public string? BfsComponentName { get; set; }
public string? BusinessActionName { get; set; }
public string? ActionLocationName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}