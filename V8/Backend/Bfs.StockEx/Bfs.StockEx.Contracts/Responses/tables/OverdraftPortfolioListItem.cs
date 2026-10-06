using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class OverdraftPortfolioListItem
    {      
        public long Id { get; set; }
public string Name { get; set; }
public string Notes { get; set; }
public decimal OverdraftPercent { get; set; }
public decimal OverdraftMax { get; set; }
public long SsPortfolioId { get; set; }
public long BrokerAgreementId { get; set; }

        public string? SsPortfolioName { get; set; }
public string? BrokerAgreementName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}