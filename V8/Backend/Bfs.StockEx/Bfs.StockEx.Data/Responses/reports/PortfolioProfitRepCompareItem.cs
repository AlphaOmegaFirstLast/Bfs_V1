using Bfs.Core.Data;

namespace Bfs.StockEx.Data
{
    public class PortfolioProfitRepCompareItem
    {
        public string? SspStock_SsPortfolioId { get; set; }
public string? SspStock_Quantity { get; set; }
public string? SspStock_StockShareId { get; set; }
public string? SspStock_AverageCost { get; set; }
public string? SspStock_CurrencyId { get; set; }
public string? CurrentPrice_Price { get; set; }
public string? CurrentPrice_Profit { get; set; }

        public string? SsPortfolioName { get; set; }
public string? StockShareName { get; set; }
public string? CurrencyName { get; set; }

        //manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
    }
}