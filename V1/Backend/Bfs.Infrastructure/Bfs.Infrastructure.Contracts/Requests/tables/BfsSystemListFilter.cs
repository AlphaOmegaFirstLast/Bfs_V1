using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Infrastructure.Contracts
{
    public class BfsSystemListFilter
    {

        public string? Name { get; set; }

        public long? BfsClientId { get; set; }
public int? SystemTemplateId { get; set; }

    }
}