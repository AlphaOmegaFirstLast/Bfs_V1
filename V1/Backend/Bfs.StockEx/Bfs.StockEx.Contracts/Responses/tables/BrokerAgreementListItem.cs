using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class BrokerAgreementListItem
    {      
        public DateTime AgreementDate { get; set; }
public long Id { get; set; }
public string Notes { get; set; }
public long InvestorId { get; set; }
public long BrokerId { get; set; }

        public string? InvestorName { get; set; }
public string? BrokerName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}