using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Data
{
    public class OverdraftPortfolioListFilter
    {
        public long? Id { get; set; }

        public string? Name { get; set; }

        public long? SsPortfolioId { get; set; }
public long? BrokerAgreementId { get; set; }

        public NumericRange? OverdraftPercent { get; set; }
public NumericRange? OverdraftMax { get; set; }

    }
}