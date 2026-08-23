using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class CurrentPriceListItem
    {      
        public string? Id { get; set; }
public string? StockShareId { get; set; }
public string? TransactionDate { get; set; }
public string? Price { get; set; }

        public string? StockShareName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}