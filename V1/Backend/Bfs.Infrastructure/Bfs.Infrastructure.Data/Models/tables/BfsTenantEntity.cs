using Bfs.Core.Interfaces;
using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using System.Collections.Generic;

namespace Bfs.Infrastructure.Data.Models
{
    public class BfsTenantEntity : IIdentifiable, ITenanted 
    {
       public long TenantId { get; set; }

        public string DbConnection {get; set;} = string.Empty ;
public bool IsDeleted {get; set;} = false ;
public long Id {get; set;} = 0 ;
public string Logo {get; set;} = string.Empty ;
public string Notes {get; set;} = string.Empty ;
public string Name {get; set;} = string.Empty ;
public string CompanyName {get; set;} = string.Empty ;

        public List<CustomField> CustomFields {get; set;} = new List<CustomField>() ;

    }
}
//Template_Start_Code_DontOverwrite_1

//Template_End_Code_DontOverwrite_1
//Template_Start_Code_DontOverwrite_2

//Template_End_Code_DontOverwrite_2

