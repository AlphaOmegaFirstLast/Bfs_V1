using Bfs.Core.Data;

namespace Bfs.Core.Services.Security
{
    public interface ITenantResourceRuleList
    {
        Task<QueryResponse<TenantResourceRuleListItem>> GetAsync(QueryRequest<TenantResourceRuleListFilter> request);
    }
}