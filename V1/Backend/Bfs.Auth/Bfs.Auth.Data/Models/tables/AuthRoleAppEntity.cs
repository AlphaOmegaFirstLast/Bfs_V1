using Bfs.Core.Interfaces;
using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using System.Collections.Generic;

namespace Bfs.Auth.Data.Models
{
    public class AuthRoleAppEntity : IIdentifiable, ITenanted 
    {
       public long TenantId { get; set; }

        public bool IsDeleted {get; set;} = false ;
public long Id {get; set;} = 0 ;

        public long AuthRoleId {get; set;} = 0 ;
public long AuthAppId {get; set;} = 0 ;

    }
}