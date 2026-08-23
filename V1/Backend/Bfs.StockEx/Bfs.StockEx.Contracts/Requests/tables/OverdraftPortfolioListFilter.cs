using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class OverdraftPortfolioListFilter
    {
        public long? Id { get; set; }

        public string? Name { get; set; }

        public long? SsPortfolioId { get; set; }

        public NumericRange? OverdraftValue { get; set; }

    }
}