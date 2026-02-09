using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Infrastructure.Contracts
{
    public class BfsFieldListFilter
    {

        public string? Field { get; set; }

        public long? BfsComponentId { get; set; }
public int? FilterTypeId { get; set; }
public int? BackendDataTypeId { get; set; }

    }
}