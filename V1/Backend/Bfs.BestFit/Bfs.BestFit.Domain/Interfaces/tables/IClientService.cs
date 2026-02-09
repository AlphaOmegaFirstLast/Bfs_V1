using Bfs.Core.Contracts;
using Bfs.BestFit.Contracts;

namespace Bfs.BestFit.Domain.Interfaces
{
    public interface IClientService
    {
        Task<Client?> GetAsync(long id);
        Task<List<Client>> GetAsync();

        Task<Client> CreateAsync(Client contract);
        Task<Client?> UpdateAsync(Client contract);
        Task DeleteAsync(long id);
        Task<Client> UploadAsync(Client contract);

        Task<QueryResponse<ClientListItem>> ListAsync(QueryRequest<ClientListFilter> contractRequest);

        //Template_Start_Code_[DontOverwrite]_1
        //Template_End_Code_[DontOverwrite]_1   
    }
}
