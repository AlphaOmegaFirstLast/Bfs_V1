using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Data
{
    public class CouponListFilter
    {
        public long? Id { get; set; }

        public string? Name { get; set; }

        public long? TradingRoomId { get; set; }
public long? StockShareId { get; set; }
public long? CouponTypeId { get; set; }
public long? CouponStatusId { get; set; }

        public NumericRange? Value { get; set; }
public DateRange? AnnounceDate { get; set; }
public DateRange? ValueDate { get; set; }
public DateRange? DueDate { get; set; }
public NumericRange? CouponPercent { get; set; }

    }
}

