using Bfs.Core.Interfaces;
using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using System.Collections.Generic;

namespace Bfs.Auth.Data.Models
{
    public class RoleAppEntity : IIdentifiable, ITenanted 
    {
       public long TenantId { get; set; }

        public bool IsDeleted {get; set;} = false ;
public long Id {get; set;} = 0 ;

        public long RoleId {get; set;} = 0 ;
public long AppId {get; set;} = 0 ;

    }
}