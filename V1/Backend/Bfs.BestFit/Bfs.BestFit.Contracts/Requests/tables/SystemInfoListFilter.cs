using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.BestFit.Contracts
{
    public class SystemInfoListFilter
    {

        public string? Name { get; set; }

        public long? ClientId { get; set; }
public int? SystemTemplateId { get; set; }

    }
}