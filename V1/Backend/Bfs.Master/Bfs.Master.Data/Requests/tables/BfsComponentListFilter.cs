using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

namespace Bfs.Master.Data
{
    public class BfsComponentListFilter
    {
        public long? Id { get; set; }

        public string? Name { get; set; }
public string? InterfaceRequired { get; set; }

        public long? BfsSystemId { get; set; }
public int? DataTypeId { get; set; }

    }
}

