using Bfs.Core.Interfaces;
using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using System.Collections.Generic;
using Bfs.Auth.Contracts;

namespace Bfs.Auth.Data.Models
{
    public class UserEntity : IIdentifiable, ITenanted ,IAuthUser
    {
       public long TenantId { get; set; }

        public bool IsDeleted {get; set;} = false ;
public long Id {get; set;} = 0 ;
public string AspNetUserId {get; set;} = string.Empty ;
public string Notes {get; set;} = string.Empty ;
public string Name {get; set;} = string.Empty ;
public string Email {get; set;} = string.Empty ;

    }
}

