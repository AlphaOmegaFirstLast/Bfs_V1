using Bfs.Core.ObjectFields;

namespace Bfs.Infrastructure.Contracts
{
    public class DeploymentLocalListItem
    {      
        public string? DeploymentLocalId { get; set; }
public string? DeploymentLocalScriptFile { get; set; }
public string? DeploymentLocalBfsSystemId { get; set; }
public string? DeploymentLocalSourceProject { get; set; }
public string? DeploymentLocalSourcePath { get; set; }
public string? DeploymentLocalPublishPath { get; set; }
public string? DeploymentLocalConfig { get; set; }
public string? DeploymentLocalEnvironmentValue { get; set; }
public string? DeploymentLocalTargetVirtualFolder { get; set; }
public string? DeploymentLocalWebSite { get; set; }
public string? DeploymentLocalAppPoolName { get; set; }
public string? DeploymentLocalPort { get; set; }
public string? DeploymentLocalHttpsRequired { get; set; }
public string? DeploymentLocalProject { get; set; }

        public string? BfsSystemName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}