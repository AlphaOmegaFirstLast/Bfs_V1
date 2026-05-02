using Bfs.Core.Interfaces;
using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using System.Collections.Generic;
using Bfs.Core.Contracts.Auth;

namespace Bfs.Auth.Data.Models
{
    public class UserRequestEntity : IIdentifiable, ITenanted ,IAspnetUserRequest
    {
       public long TenantId { get; set; }

        public bool IsDeleted {get; set;} = false ;
public long Id {get; set;} = 0 ;
public string AspNetUserId {get; set;} = string.Empty ;
public string Notes {get; set;} = string.Empty ;
public string Name {get; set;} = string.Empty ;
public string Email {get; set;} = string.Empty ;
public long UserId {get; set;} = 0 ;
public DateTime RequestDate {get; set;} = DateTime.MinValue ;
public DateTime ResponseDate {get; set;} = DateTime.MinValue ;

        public long UserRequestStatusId {get; set;} = 0 ;

    }
}

