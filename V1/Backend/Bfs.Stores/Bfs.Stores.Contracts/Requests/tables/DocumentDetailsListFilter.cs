using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Stores.Contracts
{
    public class DocumentDetailsListFilter
    {
        public long? Id { get; set; }

        public long? ProductId { get; set; }
public int? UnitId { get; set; }
public long? DocumentId { get; set; }

        public NumericRange? Quantity { get; set; }

    }
}

