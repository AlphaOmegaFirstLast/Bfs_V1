using Bfs.Core.Data;
using Bfs.Auth.Data;

namespace Bfs.Auth.Data.Interfaces
{
    public interface IAppList
    {
        Task<QueryResponse<AppListItem>> GetAsync(QueryRequest<AppListFilter> request);
    }
}

