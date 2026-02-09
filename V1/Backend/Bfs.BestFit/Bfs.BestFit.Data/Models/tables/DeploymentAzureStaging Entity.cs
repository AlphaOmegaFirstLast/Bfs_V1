using Bfs.Core.Interfaces;
using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using System.Collections.Generic;

namespace Bfs.BestFit.Data.Models
{
    public class DeploymentAzureStagingEntity : IIdentifiable, ITenanted
    {
       public long TenantId { get; set; }

        public string Project {get; set;} = string.Empty ;
public bool IsDeleted {get; set;} = false ;
public long Id {get; set;} = 0 ;
public string ScriptFile {get; set;} = string.Empty ;
public string SourceProject {get; set;} = string.Empty ;
public string SourcePath {get; set;} = string.Empty ;
public string PublishPath {get; set;} = string.Empty ;
public string Config {get; set;} = string.Empty ;
public string EnvironmentValue {get; set;} = string.Empty ;
public string TargetVirtualFolder {get; set;} = string.Empty ;
public string PublishProfilePath {get; set;} = string.Empty ;
public string AppService {get; set;} = string.Empty ;
public string ResourceGroup {get; set;} = string.Empty ;

        public long SystemInfoId {get; set;} = 0 ;

    }
}