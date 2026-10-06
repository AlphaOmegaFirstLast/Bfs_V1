using Bfs.Core.Interfaces;
using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using System.Collections.Generic;

namespace Bfs.StockEx.Data.Models
{
    public class DebitInterestEntity : IIdentifiable, ITenanted 
    {
       public long TenantId { get; set; }

        public long Id {get; set;} = 0 ;
public string Name {get; set;} = string.Empty ;
public decimal InterestPercent {get; set;} = 0 ;
public string Notes {get; set;} = string.Empty ;
public bool IsDeleted {get; set;} = false ;

        public long PaymentModeId {get; set;} = 0 ;
public long BrokerAgreementId {get; set;} = 0 ;

    }
}