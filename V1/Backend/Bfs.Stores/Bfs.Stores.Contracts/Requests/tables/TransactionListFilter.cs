using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Stores.Contracts
{
    public class TransactionListFilter
    {
        public long? Id { get; set; }

        public long? StoreId { get; set; }
public int? OperationId { get; set; }
public long? ProductId { get; set; }

        public NumericRange? Quantity { get; set; }

    }
}

