using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Data
{
    public class OverdraftPortfolioListItem
    {      
        public string? Id { get; set; }
public string? Name { get; set; }
public string? Notes { get; set; }
public string? SsPortfolioId { get; set; }
public string? OverdraftValue { get; set; }

        public string? SsPortfolioName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}