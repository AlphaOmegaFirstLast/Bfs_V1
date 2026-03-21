using Bfs.Core.Data;
using Bfs.Auth.Data;

namespace Bfs.Auth.Data.Interfaces
{
    public interface IAuthAppList
    {
        Task<QueryResponse<AuthAppListItem>> GetAsync(QueryRequest<AuthAppListFilter> request);
    }
}