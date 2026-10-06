using Bfs.Core.ObjectFields;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data.Models;

namespace Bfs.StockEx.Domain.Mapper
{
    public static class EffectTypeMapper
    {
        public static EffectType ToContract(this EffectTypeEntity entity)
        {
            var contract = new EffectType()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,

            };

            return contract;
        }

        public static List<EffectType> ToContract(this IEnumerable<EffectTypeEntity> EffectTypes)
        {
            return EffectTypes.Select(x => x.ToContract()).ToList();
        }

        public static List<EffectTypeEntity> ToEntity(this IEnumerable<EffectType> EffectTypes)
        {
            return EffectTypes.Select(x => x.ToEntity()).ToList();
        }

        public static EffectTypeEntity ToEntity(this EffectType contract, EffectTypeEntity entity = null)
        {
            var EffectTypeEntity = entity ?? new();

            EffectTypeEntity.IsDeleted= contract.IsDeleted;
EffectTypeEntity.Id= contract.Id;
EffectTypeEntity.Name= contract.Name;
EffectTypeEntity.Notes= contract.Notes;

            return EffectTypeEntity;
        }     
    }
}

