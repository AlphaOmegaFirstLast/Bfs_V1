using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Data
{
    public class InvestorBrokerFundListItem
    {      
        public long Id { get; set; }
public string Name { get; set; }
public long BrokerId { get; set; }
public long InvestorId { get; set; }
public decimal Fund { get; set; }
public DateTime FundDate { get; set; }

        public string? BrokerName { get; set; }
public string? InvestorName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}