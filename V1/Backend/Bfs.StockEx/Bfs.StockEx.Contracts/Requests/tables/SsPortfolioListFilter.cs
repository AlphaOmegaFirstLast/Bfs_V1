using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class SsPortfolioListFilter
    {
        public long? Id { get; set; }

        public string? Name { get; set; }

        public long? BrokerId { get; set; }
public long? InvestorId { get; set; }

        public NumericRange? Interest { get; set; }

    }
}

