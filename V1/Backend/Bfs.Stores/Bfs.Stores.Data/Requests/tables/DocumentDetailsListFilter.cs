using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

namespace Bfs.Stores.Data
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

