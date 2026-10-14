using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class BrokerAgreementListItem
    {              
        public string AutoCompleteName { get; set; }
        public string LookupName { get; set; }

        public DateTime AgreementDate { get; set; }
public long Id { get; set; }
public string Notes { get; set; }
public long InvestorId { get; set; }
public long BrokerId { get; set; }

        public string? InvestorName { get; set; }
public string? BrokerName { get; set; }

   }
}