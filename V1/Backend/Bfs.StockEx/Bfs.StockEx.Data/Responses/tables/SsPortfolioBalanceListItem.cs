using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Data
{
    public class SsPortfolioBalanceListItem
    {      
        public long Id { get; set; }
public string Name { get; set; }
public string Notes { get; set; }
public long SsPortfolioId { get; set; }
public decimal Balance { get; set; }
public long CurrencyId { get; set; }

        public string? SsPortfolioName { get; set; }
public string? CurrencyName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}

