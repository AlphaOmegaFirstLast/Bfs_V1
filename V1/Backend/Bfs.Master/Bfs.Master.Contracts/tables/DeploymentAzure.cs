using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.Master.Contracts
{
    public class DeploymentAzure : IIdentifiable ,IDeploymentAzure
    {
        ///<Summary>
        /// DeploymentAzure IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// DeploymentAzure ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// DeploymentAzure ScriptFile.
        ///</Summary>
        public string ScriptFile {get; set;} = string.Empty ;
///<Summary>
        /// DeploymentAzure SourceProject.
        ///</Summary>
        public string SourceProject {get; set;} = string.Empty ;
///<Summary>
        /// DeploymentAzure SourcePath.
        ///</Summary>
        public string SourcePath {get; set;} = string.Empty ;
///<Summary>
        /// DeploymentAzure PublishPath.
        ///</Summary>
        public string PublishPath {get; set;} = string.Empty ;
///<Summary>
        /// DeploymentAzure Config.
        ///</Summary>
        public string Config {get; set;} = string.Empty ;
///<Summary>
        /// DeploymentAzure EnvironmentValue.
        ///</Summary>
        public string EnvironmentValue {get; set;} = string.Empty ;
///<Summary>
        /// DeploymentAzure TargetVirtualDir.
        ///</Summary>
        public string TargetVirtualDir {get; set;} = string.Empty ;
///<Summary>
        /// DeploymentAzure PublishProfilePath.
        ///</Summary>
        public string PublishProfilePath {get; set;} = string.Empty ;
///<Summary>
        /// DeploymentAzure AppService.
        ///</Summary>
        public string AppService {get; set;} = string.Empty ;
///<Summary>
        /// DeploymentAzure ResourceGroup.
        ///</Summary>
        public string ResourceGroup {get; set;} = string.Empty ;

        ///<Summary>
        /// DeploymentAzure BestFit System.
        ///</Summary>
        public long BfsSystemId {get; set;} = 0 ;

    }
}