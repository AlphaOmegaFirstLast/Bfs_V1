using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Master.Contracts
{
    public class BfsFieldListFilter
    {
        public long? Id { get; set; }

        public string? Field { get; set; }

        public long? BfsComponentId { get; set; }
public int? FilterTypeId { get; set; }
public int? BackendDataTypeId { get; set; }

    }
}

