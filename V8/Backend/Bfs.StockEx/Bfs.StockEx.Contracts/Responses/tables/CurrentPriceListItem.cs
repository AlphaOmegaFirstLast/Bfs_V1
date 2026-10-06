using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class CurrentPriceListItem
    {      
        public long Id { get; set; }
public long StockShareId { get; set; }
public DateTime TransactionDate { get; set; }
public decimal Price { get; set; }

        public string? StockShareName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}