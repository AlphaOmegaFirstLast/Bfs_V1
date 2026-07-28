using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Stores.Contracts
{
    public class StoreListFilter
    {
        public long? Id { get; set; }

        public string? Name { get; set; }

        public long? AreaId { get; set; }

    }
}

