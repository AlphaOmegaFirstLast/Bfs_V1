using Bfs.Core.ObjectFields;

namespace Bfs.Master.Contracts
{
    public class BfsTenantSystemListItem
    {      
        public long Id { get; set; }
public long BfsTenantId { get; set; }
public long BfsSystemId { get; set; }

        public string? BfsTenantName { get; set; }

        public string? BfsSystemName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}

