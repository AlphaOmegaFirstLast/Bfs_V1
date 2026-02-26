using Bfs.Core.Interfaces;
using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using System.Collections.Generic;

namespace Bfs.Infrastructure.Data.Models
{
    public class BfsClientSystemEntity : IIdentifiable, ITenanted
    {
       public long TenantId { get; set; }

        public bool IsDeleted {get; set;} = false ;
public long Id {get; set;} = 0 ;

        public long BfsClientId {get; set;} = 0 ;
public long BfsSystemId {get; set;} = 0 ;

    }
}
//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1

