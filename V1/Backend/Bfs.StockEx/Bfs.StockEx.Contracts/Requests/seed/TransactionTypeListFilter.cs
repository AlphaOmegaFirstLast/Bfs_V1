using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
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

