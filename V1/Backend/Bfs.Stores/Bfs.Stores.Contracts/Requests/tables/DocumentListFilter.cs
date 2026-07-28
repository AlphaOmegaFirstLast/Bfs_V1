using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Stores.Contracts
{
    public class DocumentListFilter
    {
        public long? Id { get; set; }

        public string? Name { get; set; }

        public long? StoreId { get; set; }
public int? OperationId { get; set; }

        public DateRange? ResponseDate { get; set; }

    }
}

