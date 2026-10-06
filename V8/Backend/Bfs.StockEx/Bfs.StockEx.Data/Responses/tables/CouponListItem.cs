using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Data
{
    public class CouponListItem
    {      
        public long Id { get; set; }
public long StockShareId { get; set; }
public long CouponTypeId { get; set; }
public decimal Value { get; set; }
public DateTime AnnounceDate { get; set; }
public DateTime ValueDate { get; set; }
public DateTime DueDate { get; set; }
public decimal CouponPercent { get; set; }

        public string? TradingRoomName { get; set; }
public string? StockShareName { get; set; }
public string? CouponTypeName { get; set; }
public string? CouponStatusName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}