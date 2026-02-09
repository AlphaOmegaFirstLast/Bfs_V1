using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.BestFit.Contracts
{
    public class TableFieldListFilter
    {

        public string? Field { get; set; }

        public long? ComponentId { get; set; }
public int? FilterTypeId { get; set; }
public int? BackendDataTypeId { get; set; }
public int? FormControlTypeId { get; set; }

    }
}