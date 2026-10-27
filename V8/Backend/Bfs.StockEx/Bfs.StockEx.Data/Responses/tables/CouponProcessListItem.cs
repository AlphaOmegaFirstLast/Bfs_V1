using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Data
{
    public class CouponProcessListItem
    {              
        public string AutoCompleteName { get; set; }
        public string LookupName { get; set; }

        public long Id { get; set; }
public long CouponId { get; set; }
public DateTime ProcessDate { get; set; }
public long ProcessStatusId { get; set; }

        public string? CouponName { get; set; }
public string? ProcessStatusName { get; set; }

   }
}