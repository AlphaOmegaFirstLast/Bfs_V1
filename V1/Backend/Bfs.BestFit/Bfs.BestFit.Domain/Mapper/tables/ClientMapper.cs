using Bfs.Core.ObjectFields;
using Bfs.BestFit.Contracts;
using Bfs.BestFit.Data.Models;

namespace Bfs.BestFit.Domain.Mapper
{
    public static class ClientMapper
    {
        public static Client ToContract(this ClientEntity entity)
        {
            var contract = new Client()
            {
               IsDeleted= entity.IsDeleted,
Id= entity.Id,
Name= entity.Name,
Notes= entity.Notes,
DbConnection= entity.DbConnection,

               CustomFields= entity.CustomFields.ToContract(),

            };

            return contract;
        }

        public static List<Client> ToContract(this IEnumerable<ClientEntity> Clients)
        {
            return Clients.Select(x => x.ToContract()).ToList();
        }

        public static List<ClientEntity> ToEntity(this IEnumerable<Client> Clients)
        {
            return Clients.Select(x => x.ToEntity()).ToList();
        }

        public static ClientEntity ToEntity(this Client contract, ClientEntity entity = null)
        {
            var ClientEntity = entity ?? new();

            ClientEntity.IsDeleted= contract.IsDeleted;
ClientEntity.Id= contract.Id;
ClientEntity.Name= contract.Name;
ClientEntity.Notes= contract.Notes;
ClientEntity.DbConnection= contract.DbConnection;

            ClientEntity.CustomFields= contract.CustomFields.ToEntity();

            return ClientEntity;
        }     
    }
}
