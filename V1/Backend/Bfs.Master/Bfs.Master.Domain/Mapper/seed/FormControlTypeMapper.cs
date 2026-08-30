using Bfs.Core.ObjectFields;
using Bfs.Master.Contracts;
using Bfs.Master.Data.Models;

namespace Bfs.Master.Domain.Mapper
{
    public static class FormControlTypeMapper
    {
        public static FormControlType ToContract(this FormControlTypeEntity entity)
        {
            var contract = new FormControlType()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,

            };

            return contract;
        }

        public static List<FormControlType> ToContract(this IEnumerable<FormControlTypeEntity> FormControlTypes)
        {
            return FormControlTypes.Select(x => x.ToContract()).ToList();
        }

        public static List<FormControlTypeEntity> ToEntity(this IEnumerable<FormControlType> FormControlTypes)
        {
            return FormControlTypes.Select(x => x.ToEntity()).ToList();
        }

        public static FormControlTypeEntity ToEntity(this FormControlType contract, FormControlTypeEntity entity = null)
        {
            var FormControlTypeEntity = entity ?? new();

            FormControlTypeEntity.IsDeleted= contract.IsDeleted;
FormControlTypeEntity.Id= contract.Id;
FormControlTypeEntity.Name= contract.Name;
FormControlTypeEntity.Notes= contract.Notes;

            return FormControlTypeEntity;
        }     
    }
}
