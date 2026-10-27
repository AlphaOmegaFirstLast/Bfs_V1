using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class CouponProcessListFilter
    {
        public bool? IsAutoComplete { get; set; } = false;

        public long? Id { get; set; }

        public long? CouponId { get; set; }
public long? ProcessStatusId { get; set; }

        public DateRange? ProcessDate { get; set; }

    }
}