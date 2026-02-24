using Bfs.Core.ObjectFields;

namespace Bfs.Infrastructure.Data
{
    public class DeploymentLocalListItem
    {      
        public string? Id { get; set; }
public string? ScriptFile { get; set; }
public string? BfsSystemId { get; set; }
public string? SourceProject { get; set; }
public string? SourcePath { get; set; }
public string? PublishPath { get; set; }
public string? Config { get; set; }
public string? EnvironmentValue { get; set; }
public string? TargetVirtualDir { get; set; }
public string? WebSite { get; set; }
public string? AppPoolName { get; set; }
public string? Port { get; set; }
public string? IsHttpsRequired { get; set; }

        public string? BfsSystemName { get; set; }

//manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
   }
}