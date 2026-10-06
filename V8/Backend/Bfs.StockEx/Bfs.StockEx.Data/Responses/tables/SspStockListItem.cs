using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Data
{
    public class SspStockListItem
    {      
        public long Id { get; set; }
public string Name { get; set; }
public string Notes { get; set; }
public long SsPortfolioId { get; set; }
public decimal Quantity { get; set; }
public long StockShareId { get; set; }
public decimal AverageCost { get; set; }
public long CurrencyId { get; set; }

        public string? SsPortfolioName { get; set; }
public string? StockShareName { get; set; }
public string? CurrencyName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}