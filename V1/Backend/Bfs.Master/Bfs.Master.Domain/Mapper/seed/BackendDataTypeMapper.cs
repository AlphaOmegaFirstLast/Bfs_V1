using Bfs.Core.ObjectFields;
using Bfs.Master.Contracts;
using Bfs.Master.Data.Models;

namespace Bfs.Master.Domain.Mapper
{
    public static class BackendDataTypeMapper
    {
        public static BackendDataType ToContract(this BackendDataTypeEntity entity)
        {
            var contract = new BackendDataType()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,

            };

            return contract;
        }

        public static List<BackendDataType> ToContract(this IEnumerable<BackendDataTypeEntity> BackendDataTypes)
        {
            return BackendDataTypes.Select(x => x.ToContract()).ToList();
        }

        public static List<BackendDataTypeEntity> ToEntity(this IEnumerable<BackendDataType> BackendDataTypes)
        {
            return BackendDataTypes.Select(x => x.ToEntity()).ToList();
        }

        public static BackendDataTypeEntity ToEntity(this BackendDataType contract, BackendDataTypeEntity entity = null)
        {
            var BackendDataTypeEntity = entity ?? new();

            BackendDataTypeEntity.IsDeleted= contract.IsDeleted;
BackendDataTypeEntity.Id= contract.Id;
BackendDataTypeEntity.Name= contract.Name;
BackendDataTypeEntity.Notes= contract.Notes;

            return BackendDataTypeEntity;
        }     
    }
}
