using Bfs.Core.ObjectFields;
using Bfs.Master.Contracts;
using Bfs.Master.Data.Models;

namespace Bfs.Master.Domain.Mapper
{
    public static class SystemTemplateMapper
    {
        public static SystemTemplate ToContract(this SystemTemplateEntity entity)
        {
            var contract = new SystemTemplate()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,
ProjectType= entity.ProjectType,
OutputDirectory= entity.OutputDirectory,
SolutionDirectory= entity.SolutionDirectory,
Template= entity.Template,

            };

            return contract;
        }

        public static List<SystemTemplate> ToContract(this IEnumerable<SystemTemplateEntity> SystemTemplates)
        {
            return SystemTemplates.Select(x => x.ToContract()).ToList();
        }

        public static List<SystemTemplateEntity> ToEntity(this IEnumerable<SystemTemplate> SystemTemplates)
        {
            return SystemTemplates.Select(x => x.ToEntity()).ToList();
        }

        public static SystemTemplateEntity ToEntity(this SystemTemplate contract, SystemTemplateEntity entity = null)
        {
            var SystemTemplateEntity = entity ?? new();

            SystemTemplateEntity.IsDeleted= contract.IsDeleted;
SystemTemplateEntity.Id= contract.Id;
SystemTemplateEntity.Name= contract.Name;
SystemTemplateEntity.Notes= contract.Notes;
SystemTemplateEntity.ProjectType= contract.ProjectType;
SystemTemplateEntity.OutputDirectory= contract.OutputDirectory;
SystemTemplateEntity.SolutionDirectory= contract.SolutionDirectory;
SystemTemplateEntity.Template= contract.Template;

            return SystemTemplateEntity;
        }     
    }
}
