using Bfs.Core.ObjectFields;

namespace Bfs.StockEx.Data
{
    public class CreditInterestListItem
    {      
        public long Id { get; set; }
public decimal InterestPercent { get; set; }
public long PaymentModeId { get; set; }
public long BrokerAgreementId { get; set; }
public string Notes { get; set; }

        public string? PaymentModeName { get; set; }
public string? BrokerAgreementName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}