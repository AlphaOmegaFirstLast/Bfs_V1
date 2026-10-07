using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class SsPortfolioListItem
    {              
        public string AutoCompleteName { get; set; }
        public string LookupName { get; set; }

        public long Id { get; set; }
public string Name { get; set; }
public string Notes { get; set; }
public long BrokerId { get; set; }
public long InvestorId { get; set; }

        public string? BrokerName { get; set; }
public string? InvestorName { get; set; }

   }
}