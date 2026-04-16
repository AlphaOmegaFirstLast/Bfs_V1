using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

namespace Bfs.Stores.Data
{
    public class TransactionListFilter
    {

        public long? StoreId { get; set; }
public int? OperationId { get; set; }
public long? ProductId { get; set; }

        public NumericRange? Quantity { get; set; }

    }
}