using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Infrastructure.Contracts
{
    public class DeploymentLocal : IIdentifiable
    {
        ///<Summary>
        /// DeploymentLocal IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// DeploymentLocal ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// DeploymentLocal ScriptFile.
        ///</Summary>
        public string ScriptFile {get; set;} = string.Empty ;
///<Summary>
        /// DeploymentLocal SourceProject.
        ///</Summary>
        public string SourceProject {get; set;} = string.Empty ;
///<Summary>
        /// DeploymentLocal SourcePath.
        ///</Summary>
        public string SourcePath {get; set;} = string.Empty ;
///<Summary>
        /// DeploymentLocal PublishPath.
        ///</Summary>
        public string PublishPath {get; set;} = string.Empty ;
///<Summary>
        /// DeploymentLocal Config.
        ///</Summary>
        public string Config {get; set;} = string.Empty ;
///<Summary>
        /// DeploymentLocal EnvironmentValue.
        ///</Summary>
        public string EnvironmentValue {get; set;} = string.Empty ;
///<Summary>
        /// DeploymentLocal TargetVirtualFolder.
        ///</Summary>
        public string TargetVirtualFolder {get; set;} = string.Empty ;
///<Summary>
        /// DeploymentLocal WebSite.
        ///</Summary>
        public string WebSite {get; set;} = string.Empty ;
///<Summary>
        /// DeploymentLocal AppPoolName.
        ///</Summary>
        public string AppPoolName {get; set;} = string.Empty ;
///<Summary>
        /// DeploymentLocal Port.
        ///</Summary>
        public string Port {get; set;} = string.Empty ;
///<Summary>
        /// DeploymentLocal isHttpsRequired.
        ///</Summary>
        public bool HttpsRequired {get; set;} = false ;
///<Summary>
        /// DeploymentLocal Project.
        ///</Summary>
        public string Project {get; set;} = string.Empty ;

        ///<Summary>
        /// DeploymentLocal System Info.
        ///</Summary>
        public long BfsSystemId {get; set;} = 0 ;

    }
}