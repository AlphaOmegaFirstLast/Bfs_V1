using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class SspStockListItem
    {      
        public string? Id { get; set; }
public string? Name { get; set; }
public string? Notes { get; set; }
public string? SsPortfolioId { get; set; }
public string? Quantity { get; set; }
public string? StockShareId { get; set; }
public string? AverageCost { get; set; }

        public string? SsPortfolioName { get; set; }
public string? StockShareName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}

