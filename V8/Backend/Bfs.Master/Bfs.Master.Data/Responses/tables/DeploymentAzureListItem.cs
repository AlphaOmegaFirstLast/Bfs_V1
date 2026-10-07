using Bfs.Core.ObjectFields;

namespace Bfs.Master.Data
{
    public class DeploymentAzureListItem
    {              
        public string AutoCompleteName { get; set; }
        public string LookupName { get; set; }

        public long Id { get; set; }
public string ScriptFile { get; set; }
public long BfsSystemId { get; set; }
public string SourceProject { get; set; }
public string SourcePath { get; set; }
public string PublishPath { get; set; }
public string Config { get; set; }
public string EnvironmentValue { get; set; }
public string TargetVirtualDir { get; set; }
public string PublishProfilePath { get; set; }
public string AppService { get; set; }
public string ResourceGroup { get; set; }

        public string? BfsSystemName { get; set; }

   }
}