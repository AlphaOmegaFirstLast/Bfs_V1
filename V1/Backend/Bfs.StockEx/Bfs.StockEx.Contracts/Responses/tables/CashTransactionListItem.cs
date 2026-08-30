using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class CashTransactionListItem
    {      
        public long Id { get; set; }
public string Name { get; set; }
public string Notes { get; set; }
public long SspTransactionId { get; set; }
public long SsPortfolioId { get; set; }
public string Source { get; set; }
public DateTime SourceDate { get; set; }
public DateTime TransactionDate { get; set; }
public decimal Value { get; set; }
public int TransactionTypeId { get; set; }
public long ExpensesTypeId { get; set; }
public long CurrencyId { get; set; }

        public string? SspTransactionName { get; set; }
public string? SsPortfolioName { get; set; }
public string? TransactionTypeName { get; set; }
public string? ExpensesTypeName { get; set; }
public string? CurrencyName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}

