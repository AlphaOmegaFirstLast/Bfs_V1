using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

namespace Bfs.Master.Data
{
    public class BfsManualCodeListFilter
    {
        public long? Id { get; set; }

        public string? Name { get; set; }
public string? FileName { get; set; }

        public long? BfsSystemId { get; set; }
public long? BfsComponentId { get; set; }

    }
}