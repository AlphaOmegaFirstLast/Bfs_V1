using Bfs.Core.Interfaces;
using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using System.Collections.Generic;

namespace Bfs.StockEx.Data.Models
{
    public class BrokerEntity : IIdentifiable, ITenanted 
    {
       public long TenantId { get; set; }

        public long Id {get; set;} = 0 ;
public bool IsDeleted {get; set;} = false ;
public string Code {get; set;} = string.Empty ;
public string Name {get; set;} = string.Empty ;

        public long TradingRoomId {get; set;} = 0 ;

    }
}