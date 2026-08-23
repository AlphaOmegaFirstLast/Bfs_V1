using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Data
{
    public class CashTransactionListItem
    {      
        public string? Id { get; set; }
public string? Name { get; set; }
public string? Notes { get; set; }
public string? SspTransactionId { get; set; }
public string? SsPortfolioId { get; set; }
public string? Source { get; set; }
public string? SourceDate { get; set; }
public string? TransactionDate { get; set; }
public string? Value { get; set; }
public string? TransactionTypeId { get; set; }
public string? ExpensesTypeId { get; set; }

        public string? SspTransactionName { get; set; }
public string? SsPortfolioName { get; set; }
public string? TransactionTypeName { get; set; }
public string? ExpensesTypeName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}

