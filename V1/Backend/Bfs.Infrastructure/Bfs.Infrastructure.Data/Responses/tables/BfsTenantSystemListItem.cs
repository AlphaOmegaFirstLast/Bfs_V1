using Bfs.Core.ObjectFields;

namespace Bfs.Infrastructure.Data
{
    public class BfsTenantSystemListItem
    {      
        public string? Id { get; set; }
public string? BfsTenantId { get; set; }
public string? BfsSystemId { get; set; }

        public string? BfsTenantName { get; set; }
public string? BfsSystemName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}