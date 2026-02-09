using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

namespace Bfs.Infrastructure.Data
{
    public class BfsFieldListFilter
    {

        public string? Field { get; set; }

        public long? BfsComponentId { get; set; }
public int? FilterTypeId { get; set; }
public int? BackendDataTypeId { get; set; }

    }
}