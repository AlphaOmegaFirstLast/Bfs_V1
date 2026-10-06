using Bfs.Core.Data;
using Bfs.Master.Data;

namespace Bfs.Master.Data.Interfaces
{
    public interface IAggregateTypeList
    {
        Task<QueryResponse<AggregateTypeListItem>> GetAsync(QueryRequest<AggregateTypeListFilter> request);
    }
}