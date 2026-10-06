using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class OverdraftPortfolioListFilter
    {
        public bool? IsAutoComplete { get; set; } = false;

        public long? Id { get; set; }

        public string? Name { get; set; }

        public long? SsPortfolioId { get; set; }
public long? BrokerAgreementId { get; set; }

        public NumericRange? OverdraftPercent { get; set; }
public NumericRange? OverdraftMax { get; set; }

    }
}