using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

namespace Bfs.Stores.Data
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

