using Bfs.Core.ObjectFields;
using Bfs.Stores.Contracts;
using Bfs.Stores.Data.Models;

namespace Bfs.Stores.Domain.Mapper
{
    public static class DocumentDetailsMapper
    {
        public static DocumentDetails ToContract(this DocumentDetailsEntity entity)
        {
            var contract = new DocumentDetails()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Quantity= entity.Quantity,
Notes= entity.Notes,

               ProductId= entity.ProductId,
UnitId= entity.UnitId,
DocumentId= entity.DocumentId,

            };

            return contract;
        }

        public static List<DocumentDetails> ToContract(this IEnumerable<DocumentDetailsEntity> DocumentDetailss)
        {
            return DocumentDetailss.Select(x => x.ToContract()).ToList();
        }

        public static List<DocumentDetailsEntity> ToEntity(this IEnumerable<DocumentDetails> DocumentDetailss)
        {
            return DocumentDetailss.Select(x => x.ToEntity()).ToList();
        }

        public static DocumentDetailsEntity ToEntity(this DocumentDetails contract, DocumentDetailsEntity entity = null)
        {
            var DocumentDetailsEntity = entity ?? new();

            DocumentDetailsEntity.IsDeleted= contract.IsDeleted;
DocumentDetailsEntity.Id= contract.Id;
DocumentDetailsEntity.Quantity= contract.Quantity;
DocumentDetailsEntity.Notes= contract.Notes;

            DocumentDetailsEntity.ProductId= contract.ProductId;
DocumentDetailsEntity.UnitId= contract.UnitId;
DocumentDetailsEntity.DocumentId= contract.DocumentId;

            return DocumentDetailsEntity;
        }     
    }
}

