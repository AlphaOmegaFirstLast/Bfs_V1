using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class SsPortfolioListItem
    {      
        public string? Id { get; set; }
public string? Name { get; set; }
public string? Notes { get; set; }
public string? BrokerId { get; set; }
public string? InvestorId { get; set; }

        public string? BrokerName { get; set; }
public string? InvestorName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}

