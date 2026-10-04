using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class DebitInterestListFilter
    {
        public long? Id { get; set; }

        public string? Name { get; set; }
public string? Notes { get; set; }

        public long? PaymentModeId { get; set; }
public long? BrokerAgreementId { get; set; }

        public NumericRange? InterestPercent { get; set; }

    }
}