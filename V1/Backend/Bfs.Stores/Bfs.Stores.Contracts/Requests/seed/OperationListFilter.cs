using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Stores.Contracts
{
    public class OperationListFilter
    {
        public long? Id { get; set; }

        public string? Name { get; set; }

        public int? EffectTypeId { get; set; }
public int? ThirdPartyTypeId { get; set; }

    }
}

