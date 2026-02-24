using Bfs.Core.ObjectFields;

namespace Bfs.Infrastructure.Data
{
    public class DeploymentAzureListItem
    {      
        public string? Project { get; set; }
public string? Id { get; set; }
public string? ScriptFile { get; set; }
public string? BfsSystemId { get; set; }
public string? SourceProject { get; set; }
public string? SourcePath { get; set; }
public string? PublishPath { get; set; }
public string? Config { get; set; }
public string? EnvironmentValue { get; set; }
public string? TargetVirtualDir { get; set; }
public string? PublishProfilePath { get; set; }
public string? AppService { get; set; }
public string? ResourceGroup { get; set; }

        public string? BfsSystemName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}