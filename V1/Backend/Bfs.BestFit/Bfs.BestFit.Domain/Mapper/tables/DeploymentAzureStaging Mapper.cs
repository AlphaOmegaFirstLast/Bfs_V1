using Bfs.Core.ObjectFields;
using Bfs.BestFit.Contracts;
using Bfs.BestFit.Data.Models;

namespace Bfs.BestFit.Domain.Mapper
{
    public static class DeploymentAzureStagingMapper
    {
        public static DeploymentAzureStaging ToContract(this DeploymentAzureStagingEntity entity)
        {
            var contract = new DeploymentAzureStaging()
            {
               Project= entity.Project,
IsDeleted= entity.IsDeleted,
Id= entity.Id,
ScriptFile= entity.ScriptFile,
SourceProject= entity.SourceProject,
SourcePath= entity.SourcePath,
PublishPath= entity.PublishPath,
Config= entity.Config,
EnvironmentValue= entity.EnvironmentValue,
TargetVirtualFolder= entity.TargetVirtualFolder,
PublishProfilePath= entity.PublishProfilePath,
AppService= entity.AppService,
ResourceGroup= entity.ResourceGroup,

               SystemInfoId= entity.SystemInfoId,

            };

            return contract;
        }

        public static List<DeploymentAzureStaging> ToContract(this IEnumerable<DeploymentAzureStagingEntity> DeploymentAzureStagings)
        {
            return DeploymentAzureStagings.Select(x => x.ToContract()).ToList();
        }

        public static List<DeploymentAzureStagingEntity> ToEntity(this IEnumerable<DeploymentAzureStaging> DeploymentAzureStagings)
        {
            return DeploymentAzureStagings.Select(x => x.ToEntity()).ToList();
        }

        public static DeploymentAzureStagingEntity ToEntity(this DeploymentAzureStaging contract, DeploymentAzureStagingEntity entity = null)
        {
            var DeploymentAzureStagingEntity = entity ?? new();

            DeploymentAzureStagingEntity.Project= contract.Project;
DeploymentAzureStagingEntity.IsDeleted= contract.IsDeleted;
DeploymentAzureStagingEntity.Id= contract.Id;
DeploymentAzureStagingEntity.ScriptFile= contract.ScriptFile;
DeploymentAzureStagingEntity.SourceProject= contract.SourceProject;
DeploymentAzureStagingEntity.SourcePath= contract.SourcePath;
DeploymentAzureStagingEntity.PublishPath= contract.PublishPath;
DeploymentAzureStagingEntity.Config= contract.Config;
DeploymentAzureStagingEntity.EnvironmentValue= contract.EnvironmentValue;
DeploymentAzureStagingEntity.TargetVirtualFolder= contract.TargetVirtualFolder;
DeploymentAzureStagingEntity.PublishProfilePath= contract.PublishProfilePath;
DeploymentAzureStagingEntity.AppService= contract.AppService;
DeploymentAzureStagingEntity.ResourceGroup= contract.ResourceGroup;

            DeploymentAzureStagingEntity.SystemInfoId= contract.SystemInfoId;

            return DeploymentAzureStagingEntity;
        }     
    }
}
