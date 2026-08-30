using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Data
{
    public class TransactionTypeListFilter
    {
        public long? Id { get; set; }

        public string? Name { get; set; }

        public int? EffectTypeId { get; set; }
public int? StockEntityTypeId { get; set; }
public int? CalculationMethodId { get; set; }
public int? SourceTypeId { get; set; }
public int? StockFieldTypeId { get; set; }
public int? NextTransactionTypeId { get; set; }

    }
}

