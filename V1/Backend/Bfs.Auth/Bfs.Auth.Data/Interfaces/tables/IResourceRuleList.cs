using Bfs.Core.Data;
using Bfs.Auth.Data;

namespace Bfs.Auth.Data.Interfaces
{
    public interface IResourceRuleList
    {
        Task<QueryResponse<ResourceRuleListItem>> GetAsync(QueryRequest<ResourceRuleListFilter> request);
    }
}

