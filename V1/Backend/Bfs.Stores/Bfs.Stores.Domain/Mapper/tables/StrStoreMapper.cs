using Bfs.Core.ObjectFields;
using Bfs.Stores.Contracts;
using Bfs.Stores.Data.Models;

namespace Bfs.Stores.Domain.Mapper
{
    public static class StrStoreMapper
    {
        public static StrStore ToContract(this StrStoreEntity entity)
        {
            var contract = new StrStore()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,

            };

            return contract;
        }

        public static List<StrStore> ToContract(this IEnumerable<StrStoreEntity> StrStores)
        {
            return StrStores.Select(x => x.ToContract()).ToList();
        }

        public static List<StrStoreEntity> ToEntity(this IEnumerable<StrStore> StrStores)
        {
            return StrStores.Select(x => x.ToEntity()).ToList();
        }

        public static StrStoreEntity ToEntity(this StrStore contract, StrStoreEntity entity = null)
        {
            var StrStoreEntity = entity ?? new();

            StrStoreEntity.IsDeleted= contract.IsDeleted;
StrStoreEntity.Id= contract.Id;
StrStoreEntity.Name= contract.Name;
StrStoreEntity.Notes= contract.Notes;

            return StrStoreEntity;
        }     
    }
}
