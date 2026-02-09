using Bfs.Core.Interfaces;
using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using System.Collections.Generic;

namespace Bfs.BestFit.Data.Models
{
    public class CustomReportsEntity : IIdentifiable, ITenanted
    {
       public long TenantId { get; set; }

        public long Id {get; set;} = 0 ;
public string Name {get; set;} = string.Empty ;
public string Request {get; set;} = string.Empty ;
public string BaseReport {get; set;} = string.Empty ;
public bool IsPrivate {get; set;} = false ;
public bool IsDeleted {get; set;} = false ;
public string CreatedBy {get; set;} = string.Empty ;
public string Url {get; set;} = string.Empty ;

    }
}