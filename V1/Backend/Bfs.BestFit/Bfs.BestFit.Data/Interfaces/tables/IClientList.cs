using Bfs.Core.Data;
using Bfs.BestFit.Data;

namespace Bfs.BestFit.Data.Interfaces
{
    public interface IClientList
    {
        Task<QueryResponse<ClientListItem>> GetClientListAsync(QueryRequest<ClientListFilter> request);
    }
}