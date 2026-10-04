using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class SspTransactionListItem
    {      
        public long Id { get; set; }
public string Name { get; set; }
public string Notes { get; set; }
public DateTime SourceDate { get; set; }
public DateTime TransactionDate { get; set; }
public string Source { get; set; }
public long SsPortfolioId { get; set; }
public int TransactionTypeId { get; set; }
public decimal Quantity { get; set; }
public decimal Price { get; set; }
public long StockShareId { get; set; }
public decimal ToQuantity { get; set; }
public long ToPortfolioId { get; set; }
public long CurrencyId { get; set; }

        public string? TransactionTypeName { get; set; }
public string? StockShareName { get; set; }
public string? CurrencyName { get; set; }

        public string? SsPortfolioName { get; set; }
public string? ToPortfolioName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}