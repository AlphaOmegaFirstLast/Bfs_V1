using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

namespace Bfs.Master.Data
{
    public class BfsTenantSystemListFilter
    {
        public bool? IsAutoComplete { get; set; } = false ;

        public long? Id { get; set; }

        public long? BfsTenantId { get; set; }
public long? BfsSystemId { get; set; }

    }
}