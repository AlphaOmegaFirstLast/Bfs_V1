using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Data
{
    public class DebitInterestListFilter
    {
        public bool? IsAutoComplete { get; set; } = false ;

        public long? Id { get; set; }

        public string? Name { get; set; }
public string? Notes { get; set; }

        public long? PaymentModeId { get; set; }
public long? BrokerAgreementId { get; set; }

        public NumericRange? InterestPercent { get; set; }

    }
}