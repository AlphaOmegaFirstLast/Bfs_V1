using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

namespace Bfs.Master.Data
{
    public class BfsFieldListFilter
    {
        public long? Id { get; set; }

        public string? Field { get; set; }

        public int? FilterTypeId { get; set; }
public int? BackendDataTypeId { get; set; }

        public long? BfsComponentId { get; set; }

    }
}

