using Bfs.Core.Interfaces;
using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using System.Collections.Generic;

namespace Bfs.StockEx.Data.Models
{
    public class CouponProcessEntity : IIdentifiable, ITenanted 
    {
       public long TenantId { get; set; }

        public bool IsDeleted {get; set;} = false ;
public long Id {get; set;} = 0 ;
public string Notes {get; set;} = string.Empty ;
public DateTime ProcessDate {get; set;} = DateTime.MinValue ;

        public long CouponId {get; set;} = 0 ;
public long ProcessStatusId {get; set;} = 0 ;

    }
}