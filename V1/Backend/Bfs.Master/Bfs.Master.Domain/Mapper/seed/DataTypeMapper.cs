using Bfs.Core.ObjectFields;
using Bfs.Master.Contracts;
using Bfs.Master.Data.Models;

namespace Bfs.Master.Domain.Mapper
{
    public static class DataTypeMapper
    {
        public static DataType ToContract(this DataTypeEntity entity)
        {
            var contract = new DataType()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,

            };

            return contract;
        }

        public static List<DataType> ToContract(this IEnumerable<DataTypeEntity> DataTypes)
        {
            return DataTypes.Select(x => x.ToContract()).ToList();
        }

        public static List<DataTypeEntity> ToEntity(this IEnumerable<DataType> DataTypes)
        {
            return DataTypes.Select(x => x.ToEntity()).ToList();
        }

        public static DataTypeEntity ToEntity(this DataType contract, DataTypeEntity entity = null)
        {
            var DataTypeEntity = entity ?? new();

            DataTypeEntity.IsDeleted= contract.IsDeleted;
DataTypeEntity.Id= contract.Id;
DataTypeEntity.Name= contract.Name;
DataTypeEntity.Notes= contract.Notes;

            return DataTypeEntity;
        }     
    }
}
