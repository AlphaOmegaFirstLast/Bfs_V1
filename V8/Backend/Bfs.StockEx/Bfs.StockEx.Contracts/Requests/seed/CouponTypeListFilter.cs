using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class CouponTypeListFilter
    {
        public bool? IsAutoComplete { get; set; } = false;

        public long? Id { get; set; }

        public string? Name { get; set; }

    }
}