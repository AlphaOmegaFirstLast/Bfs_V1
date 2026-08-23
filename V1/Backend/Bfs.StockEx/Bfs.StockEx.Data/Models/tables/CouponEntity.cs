using Bfs.Core.Interfaces;
using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using System.Collections.Generic;

namespace Bfs.StockEx.Data.Models
{
    public class CouponEntity : IIdentifiable, ITenanted 
    {
       public long TenantId { get; set; }

        public bool IsDeleted {get; set;} = false ;
public long Id {get; set;} = 0 ;
public string Name {get; set;} = string.Empty ;
public string Notes {get; set;} = string.Empty ;
public decimal Value {get; set;} = 0 ;
public DateTime AnnounceDate {get; set;} = DateTime.MinValue ;
public DateTime ValueDate {get; set;} = DateTime.MinValue ;
public DateTime DueDate {get; set;} = DateTime.MinValue ;
public decimal CouponPercent {get; set;} = 0 ;

        public long TradingRoomId {get; set;} = 0 ;
public long StockShareId {get; set;} = 0 ;
public long CouponTypeId {get; set;} = 0 ;
public long CouponStatusId {get; set;} = 0 ;

    }
}

