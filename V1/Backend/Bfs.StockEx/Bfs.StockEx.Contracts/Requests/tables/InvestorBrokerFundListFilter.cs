using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class InvestorBrokerFundListFilter
    {
        public long? Id { get; set; }

        public string? Name { get; set; }

        public long? BrokerId { get; set; }
public long? InvestorId { get; set; }

        public NumericRange? Fund { get; set; }
public DateRange? FundDate { get; set; }

    }
}