using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class InvestorBrokerFundListItem
    {      
        public string? Id { get; set; }
public string? Name { get; set; }
public string? BrokerId { get; set; }
public string? InvestorId { get; set; }
public string? Fund { get; set; }
public string? FundDate { get; set; }

        public string? BrokerName { get; set; }
public string? InvestorName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}