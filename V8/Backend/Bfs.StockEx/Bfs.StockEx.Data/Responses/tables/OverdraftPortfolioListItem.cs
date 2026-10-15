using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Data
{
    public class OverdraftPortfolioListItem
    {              
        public string AutoCompleteName { get; set; }
        public string LookupName { get; set; }

        public long Id { get; set; }
public string Notes { get; set; }
public decimal OverdraftPercent { get; set; }
public decimal OverdraftMax { get; set; }
public long SsPortfolioId { get; set; }
public long BrokerAgreementId { get; set; }

        public string? SsPortfolioName { get; set; }

        public string? BrokerAgreementName { get; set; }

   }
}