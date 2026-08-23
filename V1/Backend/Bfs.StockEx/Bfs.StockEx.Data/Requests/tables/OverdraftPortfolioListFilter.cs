using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Data
{
    public class OverdraftPortfolioListFilter
    {
        public long? Id { get; set; }

        public string? Name { get; set; }

        public long? SsPortfolioId { get; set; }

        public NumericRange? OverdraftValue { get; set; }

    }
}