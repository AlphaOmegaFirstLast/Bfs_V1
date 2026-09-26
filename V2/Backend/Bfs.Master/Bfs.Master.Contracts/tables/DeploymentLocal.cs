using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Master.Contracts
{
    public class DeploymentLocal : IIdentifiable ,IDeploymentLocal
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
        /// DeploymentLocal TargetVirtualDir.
        ///</Summary>
        public string TargetVirtualDir {get; set;} = string.Empty ;
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
        /// DeploymentLocal IsHttpsRequired.
        ///</Summary>
        public bool IsHttpsRequired {get; set;} = false ;

        ///<Summary>
        /// DeploymentLocal System Info.
        ///</Summary>
        public long BfsSystemId {get; set;} = 0 ;

    }
}