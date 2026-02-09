using Bfs.Core.Interfaces;
using Bfs.Core.Contracts;
using Bfs.Core.ObjectFields;

namespace Bfs.BestFit.Contracts
{
    public class DeploymentAzureStaging : IIdentifiable
    {
        ///<Summary>
        /// DeploymentAzureStaging Project.
        ///</Summary>
        public string Project {get; set;} = string.Empty ;
///<Summary>
        /// DeploymentAzureStaging IsDeleted.
        ///</Summary>
        public bool IsDeleted {get; set;} = false ;
///<Summary>
        /// DeploymentAzureStaging ID.
        ///</Summary>
        public long Id {get; set;} = 0 ;
///<Summary>
        /// DeploymentAzureStaging ScriptFile.
        ///</Summary>
        public string ScriptFile {get; set;} = string.Empty ;
///<Summary>
        /// DeploymentAzureStaging SourceProject.
        ///</Summary>
        public string SourceProject {get; set;} = string.Empty ;
///<Summary>
        /// DeploymentAzureStaging SourcePath.
        ///</Summary>
        public string SourcePath {get; set;} = string.Empty ;
///<Summary>
        /// DeploymentAzureStaging PublishPath.
        ///</Summary>
        public string PublishPath {get; set;} = string.Empty ;
///<Summary>
        /// DeploymentAzureStaging Config.
        ///</Summary>
        public string Config {get; set;} = string.Empty ;
///<Summary>
        /// DeploymentAzureStaging EnvironmentValue.
        ///</Summary>
        public string EnvironmentValue {get; set;} = string.Empty ;
///<Summary>
        /// DeploymentAzureStaging TargetVirtualFolder.
        ///</Summary>
        public string TargetVirtualFolder {get; set;} = string.Empty ;
///<Summary>
        /// DeploymentAzureStaging PublishProfilePath.
        ///</Summary>
        public string PublishProfilePath {get; set;} = string.Empty ;
///<Summary>
        /// DeploymentAzureStaging AppService.
        ///</Summary>
        public string AppService {get; set;} = string.Empty ;
///<Summary>
        /// DeploymentAzureStaging ResourceGroup.
        ///</Summary>
        public string ResourceGroup {get; set;} = string.Empty ;

        ///<Summary>
        /// DeploymentAzureStaging System Info.
        ///</Summary>
        public long SystemInfoId {get; set;} = 0 ;

    }
}