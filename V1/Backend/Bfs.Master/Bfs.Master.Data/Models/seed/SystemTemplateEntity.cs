using Bfs.Core.Interfaces;
using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using System.Collections.Generic;

namespace Bfs.Master.Data.Models
{
    public class SystemTemplateEntity : IIdentifiable, ITenanted 
    {
       public long TenantId { get; set; }

        public bool IsDeleted {get; set;} = false ;
public long Id {get; set;} = 0 ;
public string Name {get; set;} = string.Empty ;
public string Notes {get; set;} = string.Empty ;
public string ProjectType {get; set;} = string.Empty ;
public string OutputDirectory {get; set;} = string.Empty ;
public string SolutionDirectory {get; set;} = string.Empty ;
public string Template {get; set;} = string.Empty ;

    }
}