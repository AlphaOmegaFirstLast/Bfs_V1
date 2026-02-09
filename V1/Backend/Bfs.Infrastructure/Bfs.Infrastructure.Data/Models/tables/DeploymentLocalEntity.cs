using Bfs.Core.Interfaces;
using Bfs.Core.Data;
using Bfs.Core.ObjectFields;

using System.Collections.Generic;

namespace Bfs.Infrastructure.Data.Models
{
    public class DeploymentLocalEntity : IIdentifiable, ITenanted
    {
       public long TenantId { get; set; }

        public bool IsDeleted {get; set;} = false ;
public long Id {get; set;} = 0 ;
public string ScriptFile {get; set;} = string.Empty ;
public string SourceProject {get; set;} = string.Empty ;
public string SourcePath {get; set;} = string.Empty ;
public string PublishPath {get; set;} = string.Empty ;
public string Config {get; set;} = string.Empty ;
public string EnvironmentValue {get; set;} = string.Empty ;
public string TargetVirtualFolder {get; set;} = string.Empty ;
public string WebSite {get; set;} = string.Empty ;
public string AppPoolName {get; set;} = string.Empty ;
public string Port {get; set;} = string.Empty ;
public bool HttpsRequired {get; set;} = false ;
public string Project {get; set;} = string.Empty ;

        public long BfsSystemId {get; set;} = 0 ;

    }
}