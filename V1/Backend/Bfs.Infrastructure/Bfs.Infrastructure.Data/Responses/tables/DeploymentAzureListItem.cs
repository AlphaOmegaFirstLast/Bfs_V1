using Bfs.Core.ObjectFields;

namespace Bfs.Infrastructure.Data
{
    public class DeploymentAzureListItem
    {      
        public string? DeploymentAzureProject { get; set; }
public string? DeploymentAzureId { get; set; }
public string? DeploymentAzureScriptFile { get; set; }
public string? DeploymentAzureBfsSystemId { get; set; }
public string? DeploymentAzureSourceProject { get; set; }
public string? DeploymentAzureSourcePath { get; set; }
public string? DeploymentAzurePublishPath { get; set; }
public string? DeploymentAzureConfig { get; set; }
public string? DeploymentAzureEnvironmentValue { get; set; }
public string? DeploymentAzureTargetVirtualFolder { get; set; }
public string? DeploymentAzurePublishProfilePath { get; set; }
public string? DeploymentAzureAppService { get; set; }
public string? DeploymentAzureResourceGroup { get; set; }

        public string? BfsSystemName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}