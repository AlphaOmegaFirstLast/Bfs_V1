using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class CashTransactionListFilter
    {
        public long? Id { get; set; }

        public string? Name { get; set; }

        public long? SspTransactionId { get; set; }
public long? SsPortfolioId { get; set; }
public int? TransactionTypeId { get; set; }
public long? ExpensesTypeId { get; set; }

        public DateRange? SourceDate { get; set; }
public DateRange? TransactionDate { get; set; }
public NumericRange? Value { get; set; }

    }
}

