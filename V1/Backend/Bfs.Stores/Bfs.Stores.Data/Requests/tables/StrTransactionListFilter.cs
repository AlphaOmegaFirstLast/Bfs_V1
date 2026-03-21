using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

namespace Bfs.Stores.Data
{
    public class StrTransactionListFilter
    {

        public long? StrStoreId { get; set; }
public int? StrOperationId { get; set; }
public long? StrProductId { get; set; }

        public NumericRange? Quantity { get; set; }

    }
}