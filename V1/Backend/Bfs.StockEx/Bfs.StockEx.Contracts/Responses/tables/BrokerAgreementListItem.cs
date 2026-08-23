using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Contracts
{
    public class BrokerAgreementListItem
    {      
        public string? AgreementDate { get; set; }
public string? Id { get; set; }
public string? Notes { get; set; }
public string? OverdraftPrcnt { get; set; }
public string? OverdraftMx { get; set; }
public string? InvestorId { get; set; }
public string? BrokerId { get; set; }
public string? SsPortfolioId { get; set; }

        public string? InvestorName { get; set; }
public string? BrokerName { get; set; }
public string? SsPortfolioName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}