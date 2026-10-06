using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Data
{
    public class CashTransactionListFilter
    {
        public bool? IsAutoComplete { get; set; } = false ;

        public long? Id { get; set; }

        public string? Name { get; set; }

        public long? SspTransactionId { get; set; }
public long? SsPortfolioId { get; set; }
public int? TransactionTypeId { get; set; }
public long? ExpensesTypeId { get; set; }
public long? CurrencyId { get; set; }

        public DateRange? SourceDate { get; set; }
public DateRange? TransactionDate { get; set; }
public NumericRange? Value { get; set; }

    }
}