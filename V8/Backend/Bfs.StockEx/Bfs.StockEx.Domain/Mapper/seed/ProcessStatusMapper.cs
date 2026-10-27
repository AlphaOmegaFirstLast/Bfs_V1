using Bfs.Core.ObjectFields;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data.Models;

namespace Bfs.StockEx.Domain.Mapper
{
    public static class ProcessStatusMapper
    {
        public static ProcessStatus ToContract(this ProcessStatusEntity entity)
        {
            var contract = new ProcessStatus()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,

            };

            return contract;
        }

        public static List<ProcessStatus> ToContract(this IEnumerable<ProcessStatusEntity> ProcessStatuss)
        {
            return ProcessStatuss.Select(x => x.ToContract()).ToList();
        }

        public static List<ProcessStatusEntity> ToEntity(this IEnumerable<ProcessStatus> ProcessStatuss)
        {
            return ProcessStatuss.Select(x => x.ToEntity()).ToList();
        }

        public static ProcessStatusEntity ToEntity(this ProcessStatus contract, ProcessStatusEntity entity = null)
        {
            var ProcessStatusEntity = entity ?? new();

            ProcessStatusEntity.IsDeleted= contract.IsDeleted;
ProcessStatusEntity.Id= contract.Id;
ProcessStatusEntity.Name= contract.Name;
ProcessStatusEntity.Notes= contract.Notes;

            return ProcessStatusEntity;
        }     
    }
}

