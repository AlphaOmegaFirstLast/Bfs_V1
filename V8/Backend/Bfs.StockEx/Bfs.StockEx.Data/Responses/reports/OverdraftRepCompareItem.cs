using Bfs.Core.Data;

namespace Bfs.StockEx.Data
{
    public class OverdraftRepCompareItem
    {
        public string? OverdraftPortfolio_Id { get; set; }
public string? OverdraftPortfolio_Notes { get; set; }
public string? OverdraftPortfolio_OverdraftPercent { get; set; }
public string? OverdraftPortfolio_OverdraftMax { get; set; }
public string? OverdraftPortfolio_SsPortfolioId { get; set; }
public string? OverdraftPortfolio_BrokerAgreementId { get; set; }

        public string? SsPortfolioName { get; set; }
public string? BrokerAgreementName { get; set; }

        //manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
    }
}