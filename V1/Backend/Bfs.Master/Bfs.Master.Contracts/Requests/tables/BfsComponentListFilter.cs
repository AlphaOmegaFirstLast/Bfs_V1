using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Master.Contracts
{
    public class BfsComponentListFilter
    {

        public string? Name { get; set; }
public string? InterfaceRequired { get; set; }

        public long? BfsSystemId { get; set; }
public int? DataTypeId { get; set; }

    }
}