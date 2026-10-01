using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class BrokerAgreementListFilter
    {
        public long? Id { get; set; }

        public string? Name { get; set; }

        public long? InvestorId { get; set; }
public long? BrokerId { get; set; }

        public DateRange? AgreementDate { get; set; }

    }
}