using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Data
{
    public class CouponListItem
    {      
        public string? Id { get; set; }
public string? StockShareId { get; set; }
public string? CouponTypeId { get; set; }
public string? Value { get; set; }
public string? AnnounceDate { get; set; }
public string? ValueDate { get; set; }
public string? DueDate { get; set; }
public string? CouponPercent { get; set; }

        public string? TradingRoomName { get; set; }
public string? StockShareName { get; set; }
public string? CouponTypeName { get; set; }
public string? CouponStatusName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}

