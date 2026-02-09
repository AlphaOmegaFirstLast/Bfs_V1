using Bfs.Core.ObjectFields;
using Bfs.Infrastructure.Contracts;
using Bfs.Infrastructure.Data.Models;

namespace Bfs.Infrastructure.Domain.Mapper
{
    public static class DeploymentLocalMapper
    {
        public static DeploymentLocal ToContract(this DeploymentLocalEntity entity)
        {
            var contract = new DeploymentLocal()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
ScriptFile= entity.ScriptFile,
SourceProject= entity.SourceProject,
SourcePath= entity.SourcePath,
PublishPath= entity.PublishPath,
Config= entity.Config,
EnvironmentValue= entity.EnvironmentValue,
TargetVirtualFolder= entity.TargetVirtualFolder,
WebSite= entity.WebSite,
AppPoolName= entity.AppPoolName,
Port= entity.Port,
HttpsRequired= entity.HttpsRequired,
Project= entity.Project,

               BfsSystemId= entity.BfsSystemId,

            };

            return contract;
        }

        public static List<DeploymentLocal> ToContract(this IEnumerable<DeploymentLocalEntity> DeploymentLocals)
        {
            return DeploymentLocals.Select(x => x.ToContract()).ToList();
        }

        public static List<DeploymentLocalEntity> ToEntity(this IEnumerable<DeploymentLocal> DeploymentLocals)
        {
            return DeploymentLocals.Select(x => x.ToEntity()).ToList();
        }

        public static DeploymentLocalEntity ToEntity(this DeploymentLocal contract, DeploymentLocalEntity entity = null)
        {
            var DeploymentLocalEntity = entity ?? new();

            DeploymentLocalEntity.IsDeleted= contract.IsDeleted;
DeploymentLocalEntity.Id= contract.Id;
DeploymentLocalEntity.ScriptFile= contract.ScriptFile;
DeploymentLocalEntity.SourceProject= contract.SourceProject;
DeploymentLocalEntity.SourcePath= contract.SourcePath;
DeploymentLocalEntity.PublishPath= contract.PublishPath;
DeploymentLocalEntity.Config= contract.Config;
DeploymentLocalEntity.EnvironmentValue= contract.EnvironmentValue;
DeploymentLocalEntity.TargetVirtualFolder= contract.TargetVirtualFolder;
DeploymentLocalEntity.WebSite= contract.WebSite;
DeploymentLocalEntity.AppPoolName= contract.AppPoolName;
DeploymentLocalEntity.Port= contract.Port;
DeploymentLocalEntity.HttpsRequired= contract.HttpsRequired;
DeploymentLocalEntity.Project= contract.Project;

            DeploymentLocalEntity.BfsSystemId= contract.BfsSystemId;

            return DeploymentLocalEntity;
        }     
    }
}
