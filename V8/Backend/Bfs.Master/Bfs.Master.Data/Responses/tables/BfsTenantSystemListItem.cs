using Bfs.Core.ObjectFields;

namespace Bfs.Master.Data
{
    public class BfsTenantSystemListItem
    {              
        public string AutoCompleteName { get; set; }
        public string LookupName { get; set; }

        public long Id { get; set; }
public long BfsTenantId { get; set; }
public long BfsSystemId { get; set; }

        public string? BfsTenantName { get; set; }
public string? BfsSystemName { get; set; }

   }
}