using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Master.Contracts
{
    public class BfsTenantSystemListFilter
    {

        public long? Id { get; set; }
        public long? BfsTenantId { get; set; }
        public long? BfsSystemId { get; set; }

    }
}