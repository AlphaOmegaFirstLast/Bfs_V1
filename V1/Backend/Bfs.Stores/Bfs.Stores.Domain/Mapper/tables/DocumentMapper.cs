using Bfs.Core.ObjectFields;
using Bfs.Stores.Contracts;
using Bfs.Stores.Data.Models;

namespace Bfs.Stores.Domain.Mapper
{
    public static class DocumentMapper
    {
        public static Document ToContract(this DocumentEntity entity)
        {
            var contract = new Document()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
ResponseDate= entity.ResponseDate,
Notes= entity.Notes,

               StoreId= entity.StoreId,
OperationId= entity.OperationId,

            };

            return contract;
        }

        public static List<Document> ToContract(this IEnumerable<DocumentEntity> Documents)
        {
            return Documents.Select(x => x.ToContract()).ToList();
        }

        public static List<DocumentEntity> ToEntity(this IEnumerable<Document> Documents)
        {
            return Documents.Select(x => x.ToEntity()).ToList();
        }

        public static DocumentEntity ToEntity(this Document contract, DocumentEntity entity = null)
        {
            var DocumentEntity = entity ?? new();

            DocumentEntity.IsDeleted= contract.IsDeleted;
DocumentEntity.Id= contract.Id;
DocumentEntity.Name= contract.Name;
DocumentEntity.ResponseDate= contract.ResponseDate;
DocumentEntity.Notes= contract.Notes;

            DocumentEntity.StoreId= contract.StoreId;
DocumentEntity.OperationId= contract.OperationId;

            return DocumentEntity;
        }     
    }
}

