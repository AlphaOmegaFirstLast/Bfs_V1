using Bfs.Core.ObjectFields;

namespace Bfs.BestFit.Contracts
{
    public class DeploymentLocalListItem
    {
        public bool IsDeleted { get; set; }
public long Id { get; set; }
public string ScriptFile { get; set; }
public string SourceProject { get; set; }
public string SourcePath { get; set; }
public string PublishPath { get; set; }
public string Config { get; set; }
public string EnvironmentValue { get; set; }
public string TargetVirtualFolder { get; set; }
public string WebSite { get; set; }
public string AppPoolName { get; set; }
public string Port { get; set; }
public bool HttpsRequired { get; set; }
public string Project { get; set; }

        public string? SystemInfo { get; set; }

        //manual: Add list output field "Name" if there is none has been generated. for lookups & filter dropdowns
    }
}