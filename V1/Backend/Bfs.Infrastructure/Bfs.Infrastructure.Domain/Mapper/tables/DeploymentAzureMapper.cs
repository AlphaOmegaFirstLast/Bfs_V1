using Bfs.Core.ObjectFields;
using Bfs.Infrastructure.Contracts;
using Bfs.Infrastructure.Data.Models;

namespace Bfs.Infrastructure.Domain.Mapper
{
    public static class DeploymentAzureMapper
    {
        public static DeploymentAzure ToContract(this DeploymentAzureEntity entity)
        {
            var contract = new DeploymentAzure()
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
TargetVirtualDir= entity.TargetVirtualDir,
PublishProfilePath= entity.PublishProfilePath,
AppService= entity.AppService,
ResourceGroup= entity.ResourceGroup,

               BfsSystemId= entity.BfsSystemId,

            };

            return contract;
        }

        public static List<DeploymentAzure> ToContract(this IEnumerable<DeploymentAzureEntity> DeploymentAzures)
        {
            return DeploymentAzures.Select(x => x.ToContract()).ToList();
        }

        public static List<DeploymentAzureEntity> ToEntity(this IEnumerable<DeploymentAzure> DeploymentAzures)
        {
            return DeploymentAzures.Select(x => x.ToEntity()).ToList();
        }

        public static DeploymentAzureEntity ToEntity(this DeploymentAzure contract, DeploymentAzureEntity entity = null)
        {
            var DeploymentAzureEntity = entity ?? new();

            DeploymentAzureEntity.Project= contract.Project;
DeploymentAzureEntity.IsDeleted= contract.IsDeleted;
DeploymentAzureEntity.Id= contract.Id;
DeploymentAzureEntity.ScriptFile= contract.ScriptFile;
DeploymentAzureEntity.SourceProject= contract.SourceProject;
DeploymentAzureEntity.SourcePath= contract.SourcePath;
DeploymentAzureEntity.PublishPath= contract.PublishPath;
DeploymentAzureEntity.Config= contract.Config;
DeploymentAzureEntity.EnvironmentValue= contract.EnvironmentValue;
DeploymentAzureEntity.TargetVirtualDir= contract.TargetVirtualDir;
DeploymentAzureEntity.PublishProfilePath= contract.PublishProfilePath;
DeploymentAzureEntity.AppService= contract.AppService;
DeploymentAzureEntity.ResourceGroup= contract.ResourceGroup;

            DeploymentAzureEntity.BfsSystemId= contract.BfsSystemId;

            return DeploymentAzureEntity;
        }     
    }
}
