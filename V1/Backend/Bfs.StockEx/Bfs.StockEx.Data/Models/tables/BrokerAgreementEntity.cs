using Bfs.Core.Interfaces;
using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using System.Collections.Generic;

namespace Bfs.StockEx.Data.Models
{
    public class BrokerAgreementEntity : IIdentifiable, ITenanted 
    {
       public long TenantId { get; set; }

        public DateTime AgreementDate {get; set;} = DateTime.MinValue ;
public bool IsDeleted {get; set;} = false ;
public long Id {get; set;} = 0 ;
public string Name {get; set;} = string.Empty ;
public string Notes {get; set;} = string.Empty ;
public decimal OverdraftPrcnt {get; set;} = 0 ;
public decimal OverdraftMx {get; set;} = 0 ;

        public long InvestorId {get; set;} = 0 ;
public long BrokerId {get; set;} = 0 ;
public long SsPortfolioId {get; set;} = 0 ;

    }
}