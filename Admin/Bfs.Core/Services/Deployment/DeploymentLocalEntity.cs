using Bfs.Core.Interfaces;
using Bfs.Core.Data;
using System.Collections.Generic;
namespace Bfs.Core.Services.Deployment
{
    public class DeploymentLocalEntity : IIdentifiable, ITenanted
    {
        public long TenantId { get; set; }
        public bool IsDeleted { get; set; }
        public long Id { get; set; }
        public long SystemInfoId { get; set; }

        public string ScriptFile { get; set; }

        public string Project { get; set; }
        public string ProjectType { get; set; }
        public string SourceProject { get; set; }
        public string SourcePath { get; set; }
        public string PublishPath { get; set; }

        public string Config { get; set; }
        public string EnvironmentValue { get; set; }
        public string TargetVirtualFolder { get; set; }

        public string WebSite { get; set; }
        public string AppPoolName { get; set; }
        public string Port { get; set; }
        public string IsHttpsRequired { get; set; }
    }
}