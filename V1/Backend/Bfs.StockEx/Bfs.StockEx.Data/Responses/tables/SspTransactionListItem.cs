using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Data
{
    public class SspTransactionListItem
    {      
        public string? Id { get; set; }
public string? Name { get; set; }
public string? Notes { get; set; }
public string? SourceDate { get; set; }
public string? TransactionDate { get; set; }
public string? Source { get; set; }
public string? SsPortfolioId { get; set; }
public string? TransactionTypeId { get; set; }
public string? Quantity { get; set; }
public string? Price { get; set; }
public string? StockShareId { get; set; }
public string? ToQuantity { get; set; }
public string? ToPortfolioId { get; set; }

        public string? SsPortfolioName { get; set; }
public string? TransactionTypeName { get; set; }
public string? StockShareName { get; set; }
public string? ToPortfolioName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}

