using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Data
{
    public class SsPortfolioBalanceListFilter
    {
        public long? Id { get; set; }

        public string? Name { get; set; }

        public long? SsPortfolioId { get; set; }

        public NumericRange? Balance { get; set; }

    }
}

