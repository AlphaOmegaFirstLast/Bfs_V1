using Bfs.Core.ObjectFields;
using Bfs.BestFit.Contracts;
using Bfs.BestFit.Data.Models;

namespace Bfs.BestFit.Domain.Mapper
{
    public static class CustomFieldDefinitionMapper
    {
        public static CustomFieldDefinition ToContract(this CustomFieldDefinitionEntity entity)
        {
            var contract = new CustomFieldDefinition()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,
DisplayName= entity.DisplayName,

               ComponentId= entity.ComponentId,

               FieldValidation= entity.FieldValidation.ToContract(),

            };

            return contract;
        }

        public static List<CustomFieldDefinition> ToContract(this IEnumerable<CustomFieldDefinitionEntity> CustomFieldDefinitions)
        {
            return CustomFieldDefinitions.Select(x => x.ToContract()).ToList();
        }

        public static List<CustomFieldDefinitionEntity> ToEntity(this IEnumerable<CustomFieldDefinition> CustomFieldDefinitions)
        {
            return CustomFieldDefinitions.Select(x => x.ToEntity()).ToList();
        }

        public static CustomFieldDefinitionEntity ToEntity(this CustomFieldDefinition contract, CustomFieldDefinitionEntity entity = null)
        {
            var CustomFieldDefinitionEntity = entity ?? new();

            CustomFieldDefinitionEntity.IsDeleted= contract.IsDeleted;
CustomFieldDefinitionEntity.Id= contract.Id;
CustomFieldDefinitionEntity.Name= contract.Name;
CustomFieldDefinitionEntity.Notes= contract.Notes;
CustomFieldDefinitionEntity.DisplayName= contract.DisplayName;

            CustomFieldDefinitionEntity.ComponentId= contract.ComponentId;

            CustomFieldDefinitionEntity.FieldValidation= contract.FieldValidation.ToEntity();

            return CustomFieldDefinitionEntity;
        }     
    }
}
